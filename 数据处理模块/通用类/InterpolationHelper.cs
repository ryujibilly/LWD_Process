/*
 * ���в�ֵ����Interpolation
 */

using System;

namespace LWD_DataProcess
{

	/**
	 * ���в�ֵ����Interpolation
	 * @version 1.0
	 */
	public class InterpolationHelper
	{
        //
        //  һԪȫ���䲻�Ⱦ��ֵ
        //  
        // @param n - ���ĸ���
        // @param x - һά���飬����Ϊn����Ÿ�����n������ֵx(i)��
        //             Ҫ��x(0)<x(1)<...<x(n-1)
        //  @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
        //             y(i) = f(x(i)), i=0,1,...,n-1
        // @param t - ���ָ���Ĳ�ֵ���ֵ
        // @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
        // 
		public static double GetValueLagrange(int n, double[] x, double[] y, double t)
		{ 
			int i,j,k,m;
			double z,s;
			// ��ֵ
			z=0.0;	    
			// ��������
			if (n<1) 
				return(z);
			if (n==1) 
			{ 
				z=y[0];
				return(z);
			}	    
			if (n==2)
			{ 
				z=(y[0]*(t-x[1])-y[1]*(t-x[0]))/(x[0]-x[1]);
				return(z);
			}	    
			// ��ʼ��ֵ
			i=0;
			while ((x[i]<t)&&(i<n)) 
				i=i+1;
	    
			k=i-4;
			if (k<0) 
				k=0;
	    
			m=i+3;
			if (m>n-1) 
				m=n-1;
			for (i=k;i<=m;i++)
			{ 
				s=1.0;
				for (j=k;j<=m;j++)
				{
					if (j!=i) 
						// �������ղ�ֵ��ʽ
						s=s*(t-x[j])/(x[i]-x[j]);
				}
				z=z+s*y[i];
			}	    
			return(z);
		}

		/**
		 * һԪȫ����Ⱦ��ֵ
		 * 
		 * @param n - ���ĸ���
		 * @param x0 - ��ŵȾ�n������е�һ������ֵ
		 * @param xStep - �Ⱦ���Ĳ���
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param t - ���ָ���Ĳ�ֵ���ֵ
		 * @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
		 */
		public static double GetValueLagrange(int n, double x0, double xStep, double[] y, double t)
		{ 
			int i,j,k,m;
			double z,s,xi,xj;
			double p,q;
	    
			// ��ֵ
			z=0.0;
	    
			// ��������
			if (n<1) 
				return(z);
			if (n==1) 
			{ 
				z=y[0]; 
				return(z);
			}
			if (n==2)
			{ 
				z=(y[1]*(t-x0)-y[0]*(t-x0-xStep))/xStep;
				return(z);
			}
	    
			// ��ʼ��ֵ
			if (t>x0)
			{ 
				p=(t-x0)/xStep; 
				i=(int)p; 
				q=(float)i;
	        
				if (p>q) 
					i=i+1;
			}
			else 
				i=0;
	    
			k=i-4;
			if (k<0) 
				k=0;
	    
			m=i+3;
			if (m>n-1) 
				m=n-1;
	    
			for (i=k;i<=m;i++)
			{ 
				s=1.0; 
				xi=x0+i*xStep;
	        
				for (j=k; j<=m; j++)
				{
					if (j!=i)
					{ 
						xj=x0+j*xStep;
						// �������ղ�ֵ��ʽ
						s=s*(t-xj)/(xi-xj);
					}
				}

				z=z+s*y[i];
			}
	    
			return(z);
		}

		/**
		 * һԪ���㲻�Ⱦ��ֵ
		 * 
		 * @param n - ���ĸ���
		 * @param x - һά���飬����Ϊn����Ÿ�����n������ֵx(i)
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param t - ���ָ���Ĳ�ֵ���ֵ
		 * @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
		 */
		public static double GetValueLagrange3(int n, double[] x, double[] y, double t)
		{ 
			int i,j,k,m;
			double z,s;
	    
			// ��ֵ
			z=0.0;
	    
			// ��������
			if (n<1) 
				return(z);
			if (n==1) 
			{ 
				z=y[0]; 
				return(z);
			}
			if (n==2)
			{ 
				z=(y[0]*(t-x[1])-y[1]*(t-x[0]))/(x[0]-x[1]);
				return(z);
			}
	    
			// ��ʼ��ֵ
			if (t<=x[1]) 
			{ 
				k=0; 
				m=2;
			}
			else if (t>=x[n-2]) 
			{ 
				k=n-3; 
				m=n-1;
			}
			else
			{ 
				k=1; 
				m=n;
				while (m-k!=1)
				{ 
					i=(k+m)/2;
	            
					if (t<x[i-1]) 
						m=i;
					else 
						k=i;
				}
	        
				k=k-1; 
				m=m-1;
	        
				if (Math.Abs(t-x[k])<Math.Abs(t-x[m])) 
					k=k-1;
				else 
					m=m+1;
			}
	    
			z=0.0;
			for (i=k;i<=m;i++)
			{ 
				s=1.0;
				for (j=k;j<=m;j++)
				{
					if (j!=i) 
						// �����߲�ֵ��ʽ
						s=s*(t-x[j])/(x[i]-x[j]);
				}

				z=z+s*y[i];
			}
	    
			return(z);
		}

		/**
		 * һԪ����Ⱦ��ֵ
		 * 
		 * @param n - ���ĸ���
		 * @param x0 - ��ŵȾ�n������е�һ������ֵ
		 * @param xStep - �Ⱦ���Ĳ���
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param t - ���ָ���Ĳ�ֵ���ֵ
		 * @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
		 */
		public static double GetValueLagrange3(int n, double x0, double xStep, double[] y, double t)
		{ 
			int i,j,k,m;
			double z,s,xi,xj;
	    
			// ��ֵ
			z=0.0;
	    
			// ��������
			if (n<1) 
				return(z);
			if (n==1) 
			{ 
				z=y[0]; 
				return(z);
			}
			if (n==2)
			{ 
				z=(y[1]*(t-x0)-y[0]*(t-x0-xStep))/xStep;
				return(z);
			}
	    
			// ��ʼ��ֵ
			if (t<=x0+xStep) 
			{ 
				k=0; 
				m=2;
			}
			else if (t>=x0+(n-3)*xStep) 
			{ 
				k=n-3; 
				m=n-1;
			}
			else
			{ 
				i=(int)((t-x0)/xStep)+1;

				if (Math.Abs(t-x0-i*xStep)>=Math.Abs(t-x0-(i-1)*xStep))
				{ 
					k=i-2; 
					m=i;
				}
				else 
				{
					k=i-1; 
					m=i+1;
				}
			}
	    
			z=0.0;
			for (i=k;i<=m;i++)
			{ 
				s=1.0; 
				xi=x0+i*xStep;

				for (j=k;j<=m;j++)
				{
					if (j!=i)
					{ 
						xj=x0+j*xStep; 
						// �����߲�ֵ��ʽ
						s=s*(t-xj)/(xi-xj);
					}
				}

				z=z+s*y[i];
			}
	    
			return(z);
		}

		/**
		 * ����ʽ���Ⱦ��ֵ
		 * 
		 * @param n - ���ĸ���
		 * @param x - һά���飬����Ϊn����Ÿ�����n������ֵx(i)
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param t - ���ָ���Ĳ�ֵ���ֵ
		 * @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
		 */
		public static double GetValuePqs(int n, double[] x, double[] y, double t)
		{ 
			int i,j,k,m,l;
			double z,h;
			double[] b = new double[8];
	    
			// ��ֵ
			z=0.0;
	    
			// ��������
			if (n<1) 
				return(z);
			if (n==1) 
			{ 
				z=y[0]; 
				return(z);
			}
	    
			// ����ʽ��ֵ
			if (n<=8) 
			{ 
				k=0; 
				m=n;
			}
			else if (t<x[4]) 
			{ 
				k=0; 
				m=8;
			}
			else if (t>x[n-5]) 
			{ 
				k=n-8; 
				m=8;
			}
			else
			{ 
				k=1; 
				j=n;
	        
				while (j-k!=1)
				{ 
					i=(k+j)/2;
					if (t<x[i-1]) 
						j=i;
					else 
						k=i;
				}
	        
				k=k-4; 
				m=8;
			}
	    
			b[0]=y[k];
			for (i=2;i<=m;i++)
			{ 
				h=y[i+k-1]; 
				l=0; 
				j=1;
	        
				while ((l==0)&&(j<=i-1))
				{ 
					if (Math.Abs(h-b[j-1])+1.0==1.0) 
						l=1;
					else 
						h=(x[i+k-1]-x[j+k-1])/(h-b[j-1]);
	              
					j=j+1;
				}
	        
				b[i-1]=h;
	        
				if (l!=0) 
					b[i-1]=1.0e+35;
			}
	    
			z=b[m-1];
			for (i=m-1;i>=1;i--) 
				z=b[i-1]+(t-x[i+k-1])/z;
	    
			return(z);
		}

		/**
		 * ����ʽ�Ⱦ��ֵ
		 * 
		 * @param n - ���ĸ���
		 * @param x0 - ��ŵȾ�n������е�һ������ֵ
		 * @param xStep - �Ⱦ���Ĳ���
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param t - ���ָ���Ĳ�ֵ���ֵ
		 * @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
		 */
		public static double GetValuePqs(int n, double x0, double xStep, double[] y, double t)
		{ 
			int i,j,k,m,l;
			double z,hh,xi,xj;
			double[] b = new double[8];
	    
			// ��ֵ
			z=0.0;
	    
			// ��������
			if (n<1) 
				return(z);
			if (n==1) 
			{ 
				z=y[0]; 
				return(z);
			}
	    
			// ����ʽ��ֵ
			if (n<=8) 
			{ 
				k=0; 
				m=n;
			}
			else if (t<(x0+4.0*xStep)) 
			{ 
				k=0; 
				m=8;
			}
			else if (t>(x0+(n-5)*xStep)) 
			{ 
				k=n-8; 
				m=8;
			}
			else 
			{ 
				k=(int)((t-x0)/xStep)-3; 
				m=8;
			}
	    
			b[0]=y[k];
			for (i=2;i<=m;i++)
			{ 
				hh=y[i+k-1]; 
				l=0; 
				j=1;
	        
				while ((l==0)&&(j<=i-1))
				{ 
					if (Math.Abs(hh-b[j-1])+1.0==1.0) 
						l=1;
					else
					{ 
						xi=x0+(i+k-1)*xStep;
						xj=x0+(j+k-1)*xStep;
						hh=(xi-xj)/(hh-b[j-1]);
					}
	        
					j=j+1;
				}

				b[i-1]=hh;
				if (l!=0) 
					b[i-1]=1.0e+35;
			}
	    
			z=b[m-1];
			for (i=m-1;i>=1;i--)
				z=b[i-1]+(t-(x0+(i+k-1)*xStep))/z;
	    
			return(z);
		}

		/**
		 * �������ز��Ⱦ��ֵ
		 * 
		 * @param n - ���ĸ���
		 * @param x - һά���飬����Ϊn����Ÿ�����n������ֵx(i)
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param dy - һά���飬����Ϊn����Ÿ�����n�����ĺ�������ֵy'(i)��
		 *             y'(i) = f'(x(i)), i=0,1,...,n-1
		 * @param t - ���ָ���Ĳ�ֵ���ֵ
		 * @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
		 */
		public static double GetValueHermite(int n, double[] x, double[] y, double[] dy, double t)
		{ 
			int i,j;
			double z,p,q,s;
	    
			// ��ֵ
			z=0.0;
	    
			// ѭ����ֵ
			for (i=1;i<=n;i++)
			{ 
				s=1.0;
	        
				for (j=1;j<=n;j++)
				{
					if (j!=i) 
						s=s*(t-x[j-1])/(x[i-1]-x[j-1]);
				}

				s=s*s;
				p=0.0;
	        
				for (j=1;j<=n;j++)
				{
					if (j!=i) 
						p=p+1.0/(x[i-1]-x[j-1]);
				}

				q=y[i-1]+(t-x[i-1])*(dy[i-1]-2.0*y[i-1]*p);
				z=z+q*s;
			}
	    
			return(z);
		}

		/**
		 * �������صȾ��ֵ
		 * 
		 * @param n - ���ĸ���
		 * @param x0 - ��ŵȾ�n������е�һ������ֵ
		 * @param xStep - �Ⱦ���Ĳ���
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param dy - һά���飬����Ϊn����Ÿ�����n�����ĺ�������ֵy'(i)��
		 *             y'(i) = f'(x(i)), i=0,1,...,n-1
		 * @param t - ���ָ���Ĳ�ֵ���ֵ
		 * @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
		 */
		public static double GetValueHermite(int n, double x0, double xStep, double[] y, double[] dy, double t)
		{ 
			int i,j;
			double z,s,p,q;
	    
			// ��ֵ
			z=0.0;
	    
			// ѭ����ֵ
			for (i=1;i<=n;i++)
			{ 
				s=1.0; 
				q=x0+(i-1)*xStep;
	        
				for (j=1;j<=n;j++)
				{ 
					p=x0+(j-1)*xStep;
					if (j!=i) 
						s=s*(t-p)/(q-p);
				}
	        
				s=s*s;
				p=0.0;
	        
				for (j=1;j<=n;j++)
				{
					if (j!=i) 
						p=p+1.0/(q-(x0+(j-1)*xStep));
				}

				q=y[i-1]+(t-q)*(dy[i-1]-2.0*y[i-1]*p);
				z=z+q*s;
			}
	    
			return(z);
		}

		/**
		 * ���ؽ𲻵Ⱦ��𲽲�ֵ
		 * 
		 * @param n - ���ĸ���
		 * @param x - һά���飬����Ϊn����Ÿ�����n������ֵx(i)
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param t - ���ָ���Ĳ�ֵ���ֵ
		 * @param eps - ���ƾ��Ȳ���
		 * @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
		 */
		public static double GetValueAitken(int n, double[] x, double[] y, double t, double eps)
		{ 
			int i,j,k,m,l=0;
			double z;
			double[] xx = new double[10];
			double[] yy = new double[10];
	    
			// ��ֵ
			z=0.0;
	    
			// ��������
			if (n<1) 
				return(z);
			if (n==1) 
			{ 
				z=y[0]; 
				return(z);
			}
	    
			// ��ʼ��ֵ
			m=10;
			if (m>n) 
				m=n;
	    
			if (t<=x[0]) 
				k=1;
			else if (t>=x[n-1]) 
				k=n;
			else
			{ 
				k=1; 
				j=n;
	        
				while ((k-j!=1)&&(k-j!=-1))
				{ 
					l=(k+j)/2;
					if (t<x[l-1]) 
						j=l;
					else 
						k=l;
				}
	        
				if (Math.Abs(t-x[l-1])>Math.Abs(t-x[j-1])) 
					k=j;
			}
	    
			j=1; 
			l=0;
	    
			for (i=1;i<=m;i++)
			{ 
				k=k+j*l;
				if ((k<1)||(k>n))
				{ 
					l=l+1; 
					j=-j; 
					k=k+j*l;
				}
	        
				xx[i-1]=x[k-1]; 
				yy[i-1]=y[k-1];
				l=l+1; 
				j=-j;
			}
	    
			i=0;
	    
			do
			{ 
				i=i+1; 
				z=yy[i];
	        
				for (j=0;j<=i-1;j++)
					z=yy[j]+(t-xx[j])*(yy[j]-z)/(xx[j]-xx[i]);
	        
				yy[i]=z;
			} while ((i!=m-1)&&(Math.Abs(yy[i]-yy[i-1])>eps));
	    
			return(z);
		}

		/**
		 * ���ؽ�Ⱦ��𲽲�ֵ
		 * 
		 * @param n - ���ĸ���
		 * @param x0 - ��ŵȾ�n������е�һ������ֵ
		 * @param xStep - �Ⱦ���Ĳ���
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param t - ���ָ���Ĳ�ֵ���ֵ
		 * @param eps - ���ƾ��Ȳ���
		 * @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
		 */
		public static double GetValueAitken(int n, double x0, double xStep, double[] y, double t, double eps)
		{ 
			int i,j,k,m,l=0;
			double z;
			double[] xx = new double[10];
			double[] yy = new double[10];
	    
			// ��ֵ
			z=0.0;
	    
			// ��������
			if (n<1) 
				return(z);
			if (n==1) 
			{ 
				z=y[0]; 
				return(z);
			}
	    
			// ��ʼ��ֵ
			m=10;
			if (m>n) 
				m=n;
	    
			if (t<=x0) 
				k=1;
			else if (t>=x0+(n-1)*xStep) 
				k=n;
			else
			{ 
				k=1; 
				j=n;
	        
				while ((k-j!=1)&&(k-j!=-1))
				{ 
					l=(k+j)/2;
	            
					if (t<x0+(l-1)*xStep) 
						j=l;
					else 
						k=l;
				}
	        
				if (Math.Abs(t-(x0+(l-1)*xStep))>Math.Abs(t-(x0+(j-1)*xStep))) 
					k=j;
			}
	    
			j=1; 
			l=0;
			for (i=1;i<=m;i++)
			{ 
				k=k+j*l;
				if ((k<1)||(k>n))
				{ 
					l=l+1; 
					j=-j; 
					k=k+j*l;
				}
	        
				xx[i-1]=x0+(k-1)*xStep; 
				yy[i-1]=y[k-1];
				l=l+1; 
				j=-j;
			}
	    
			i=0;
			do
			{ 
				i=i+1; 
				z=yy[i];
	        
				for (j=0;j<=i-1;j++)
					z=yy[j]+(t-xx[j])*(yy[j]-z)/(xx[j]-xx[i]);
	        
				yy[i]=z;
			} while ((i!=m-1)&&(Math.Abs(yy[i]-yy[i-1])>eps));
	    
			return(z);
		}

        //
        //  �⻬���Ⱦ��ֵ
        //  
        //  @param n - ���ĸ���
        //  @param x - һά���飬����Ϊn����Ÿ�����n������ֵx(i)
        //  @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
        //             y(i) = f(x(i)), i=0,1,...,n-1
        //  @param t - ���ָ���Ĳ�ֵ���ֵ
        //  @param s - һά���飬����Ϊ5������s(0)��s(1)��s(2)��s(3)�������ζ���ʽ��ϵ����
        //   		  s(4)����ָ����ֵ��t���ĺ�������ֵf(t)��k<0ʱ��������ֵ��k>=0ʱ��
        //  @param k - ���Ʋ�������k>=0����ֻ�����k��������[x(k), x(k+1)]�ϵ����ζ���ʽ��ϵ��
        //  @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
        // 
		public static double GetValueAkima(int n, double[] x, double[] y, double t, double[] s, int k)
		{ 
			int kk,m,l;
			double p,q;
			double[] u = new double[5];
	    
			// ��ֵ
			s[4]=0.0; 
			s[0]=0.0; 
			s[1]=0.0; 
			s[2]=0.0; 
			s[3]=0.0;
	    
			// ��������
			if (n<1) 
				return s[4];
			if (n==1) 
			{ 
				s[0]=y[0]; 
				s[4]=y[0]; 
				return s[4];
			}
			if (n==2)
			{ 
				s[0]=y[0]; 
				s[1]=(y[1]-y[0])/(x[1]-x[0]);
				if (k<0)
					s[4]=(y[0]*(t-x[1])-y[1]*(t-x[0]))/(x[0]-x[1]);
				return s[4];
			}
	    
			// ��ֵ
			if (k<0)
			{ 
				if (t<=x[1]) 
					kk=0;
				else if (t>=x[n-1]) 
					kk=n-2;
				else
				{ 
					kk=1; 
					m=n;
					while (((kk-m)!=1)&&((kk-m)!=-1))
					{ 
						l=(kk+m)/2;
						if (t<x[l-1]) 
							m=l;
						else 
							kk=l;
					}
	            
					kk=kk-1;
				}
			}
			else 
				kk=k;
	    
			if (kk>=n-1) 
				kk=n-2;
	    
			u[2]=(y[kk+1]-y[kk])/(x[kk+1]-x[kk]);
			if (n==3)
			{ 
				if (kk==0)
				{ 
					u[3]=(y[2]-y[1])/(x[2]-x[1]);
					u[4]=2.0*u[3]-u[2];
					u[1]=2.0*u[2]-u[3];
					u[0]=2.0*u[1]-u[2];
				}
				else
				{ 
					u[1]=(y[1]-y[0])/(x[1]-x[0]);
					u[0]=2.0*u[1]-u[2];
					u[3]=2.0*u[2]-u[1];
					u[4]=2.0*u[3]-u[2];
				}
			}
			else
			{ 
				if (kk<=1)
				{ 
					u[3]=(y[kk+2]-y[kk+1])/(x[kk+2]-x[kk+1]);
					if (kk==1)
					{ 
						u[1]=(y[1]-y[0])/(x[1]-x[0]);
						u[0]=2.0*u[1]-u[2];
	                
						if (n==4) 
							u[4]=2.0*u[3]-u[2];
						else 
							u[4]=(y[4]-y[3])/(x[4]-x[3]);
					}
					else
					{ 
						u[1]=2.0*u[2]-u[3];
						u[0]=2.0*u[1]-u[2];
						u[4]=(y[3]-y[2])/(x[3]-x[2]);
					}
				}
				else if (kk>=(n-3))
				{ 
					u[1]=(y[kk]-y[kk-1])/(x[kk]-x[kk-1]);
					if (kk==(n-3))
					{ 
						u[3]=(y[n-1]-y[n-2])/(x[n-1]-x[n-2]);
						u[4]=2.0*u[3]-u[2];
						if (n==4) 
							u[0]=2.0*u[1]-u[2];
						else 
							u[0]=(y[kk-1]-y[kk-2])/(x[kk-1]-x[kk-2]);
					}
					else
					{ 
						u[3]=2.0*u[2]-u[1];
						u[4]=2.0*u[3]-u[2];
						u[0]=(y[kk-1]-y[kk-2])/(x[kk-1]-x[kk-2]);
					}
				}
				else
				{ 
					u[1]=(y[kk]-y[kk-1])/(x[kk]-x[kk-1]);
					u[0]=(y[kk-1]-y[kk-2])/(x[kk-1]-x[kk-2]);
					u[3]=(y[kk+2]-y[kk+1])/(x[kk+2]-x[kk+1]);
					u[4]=(y[kk+3]-y[kk+2])/(x[kk+3]-x[kk+2]);
				}
			}
	    
			s[0]=Math.Abs(u[3]-u[2]);
			s[1]=Math.Abs(u[0]-u[1]);
			if ((s[0]+1.0==1.0)&&(s[1]+1.0==1.0))
				p=(u[1]+u[2])/2.0;
			else 
				p=(s[0]*u[1]+s[1]*u[2])/(s[0]+s[1]);
	    
			s[0]=Math.Abs(u[3]-u[4]);
			s[1]=Math.Abs(u[2]-u[1]);
			if ((s[0]+1.0==1.0)&&(s[1]+1.0==1.0))
				q=(u[2]+u[3])/2.0;
			else 
				q=(s[0]*u[2]+s[1]*u[3])/(s[0]+s[1]);
	    
			s[0]=y[kk];
			s[1]=p;
			s[3]=x[kk+1]-x[kk];
			s[2]=(3.0*u[2]-2.0*p-q)/s[3];
			s[3]=(q+p-2.0*u[2])/(s[3]*s[3]);
			if (k<0)
			{ 
				p=t-x[kk];
				s[4]=s[0]+s[1]*p+s[2]*p*p+s[3]*p*p*p;
			}
	    
			return s[4];
		}

        //
        // �⻬�Ⱦ��ֵ
        //  
        //  @param n - ���ĸ���
        //  @param x0 - ��ŵȾ�n������е�һ������ֵ
        //  @param xStep - �Ⱦ���Ĳ���
        //  @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
        //             y(i) = f(x(i)), i=0,1,...,n-1
        //  @param t - ���ָ���Ĳ�ֵ���ֵ
        //  @param s - һά���飬����Ϊ5������s(0)��s(1)��s(2)��s(3)�������ζ���ʽ��ϵ����s(4)����ָ����ֵ��t���ĺ�������ֵf(t) k<0 ʱ ������ֵk>=0ʱ
        //  @param k - ���Ʋ�������k>=0����ֻ�����k��������[x(k), x(k+1)]�ϵ����ζ���ʽ��ϵ��
        //  @return double �ͣ�ָ���Ĳ�ָ��t�ĺ�������ֵf(t)
        // */
		public static double GetValueAkima(int n, double x0, double xStep, double[] y, double t, double[] s, int k)
		{ 
			int kk,m,l;
			double[] u = new double[5];
			double p,q;
	    
			// ��ֵ
			s[4]=0.0; 
			s[0]=0.0; 
			s[1]=0.0; 
			s[2]=0.0; 
			s[3]=0.0;
	    
			// ��������
			if (n<1) 
				return s[4];
			if (n==1) 
			{ 
				s[0]=y[0]; 
				s[4]=y[0]; 
				return s[4];
			}
			if (n==2)
			{ 
				s[0]=y[0]; 
				s[1]=(y[1]-y[0])/xStep;
				if (k<0)
					s[4]=(y[1]*(t-x0)-y[0]*(t-x0-xStep))/xStep;
				return s[4];
			}
	    
			// ��ֵ
			if (k<0)
			{ 
				if (t<=x0+xStep) 
					kk=0;
				else if (t>=x0+(n-1)*xStep) 
					kk=n-2;
				else
				{ 
					kk=1; 
					m=n;
					while (((kk-m)!=1)&&((kk-m)!=-1))
					{ 
						l=(kk+m)/2;
						if (t<x0+(l-1)*xStep) 
							m=l;
						else 
							kk=l;
					}
	            
					kk=kk-1;
				}
			}
			else 
				kk=k;
	    
			if (kk>=n-1) 
				kk=n-2;
	    
			u[2]=(y[kk+1]-y[kk])/xStep;
			if (n==3)
			{ 
				if (kk==0)
				{ 
					u[3]=(y[2]-y[1])/xStep;
					u[4]=2.0*u[3]-u[2];
					u[1]=2.0*u[2]-u[3];
					u[0]=2.0*u[1]-u[2];
				}
				else
				{ 
					u[1]=(y[1]-y[0])/xStep;
					u[0]=2.0*u[1]-u[2];
					u[3]=2.0*u[2]-u[1];
					u[4]=2.0*u[3]-u[2];
				}
			}
			else
			{ 
				if (kk<=1)
				{ 
					u[3]=(y[kk+2]-y[kk+1])/xStep;
					if (kk==1)
					{ 
						u[1]=(y[1]-y[0])/xStep;
						u[0]=2.0*u[1]-u[2];
						if (n==4) 
							u[4]=2.0*u[3]-u[2];
						else 
							u[4]=(y[4]-y[3])/xStep;
					}
					else
					{ 
						u[1]=2.0*u[2]-u[3];
						u[0]=2.0*u[1]-u[2];
						u[4]=(y[3]-y[2])/xStep;
					}
				}
				else if (kk>=(n-3))
				{ 
					u[1]=(y[kk]-y[kk-1])/xStep;
					if (kk==(n-3))
					{ 
						u[3]=(y[n-1]-y[n-2])/xStep;
						u[4]=2.0*u[3]-u[2];
						if (n==4) 
							u[0]=2.0*u[1]-u[2];
						else 
							u[0]=(y[kk-1]-y[kk-2])/xStep;
					}
					else
					{ 
						u[3]=2.0*u[2]-u[1];
						u[4]=2.0*u[3]-u[2];
						u[0]=(y[kk-1]-y[kk-2])/xStep;
					}
				}
				else
				{ 
					u[1]=(y[kk]-y[kk-1])/xStep;
					u[0]=(y[kk-1]-y[kk-2])/xStep;
					u[3]=(y[kk+2]-y[kk+1])/xStep;
					u[4]=(y[kk+3]-y[kk+2])/xStep;
				}
			}
	    
			s[0]=Math.Abs(u[3]-u[2]);
			s[1]=Math.Abs(u[0]-u[1]);
			if ((s[0]+1.0==1.0)&&(s[1]+1.0==1.0))
				p=(u[1]+u[2])/2.0;
			else 
				p=(s[0]*u[1]+s[1]*u[2])/(s[0]+s[1]);
	    
			s[0]=Math.Abs(u[3]-u[4]);
			s[1]=Math.Abs(u[2]-u[1]);
			if ((s[0]+1.0==1.0)&&(s[1]+1.0==1.0))
				q=(u[2]+u[3])/2.0;
			else 
				q=(s[0]*u[2]+s[1]*u[3])/(s[0]+s[1]);
	    
			s[0]=y[kk];
			s[1]=p;
			s[3]=xStep;
			s[2]=(3.0*u[2]-2.0*p-q)/s[3];
			s[3]=(q+p-2.0*u[2])/(s[3]*s[3]);
	    
			if (k<0)
			{ 
				p=t-(x0+kk*xStep);
				s[4]=s[0]+s[1]*p+s[2]*p*p+s[3]*p*p*p;
			}
	    
			return s[4];
		}

		/**
		 * ��һ�ֱ߽���������������������ֵ��΢�������
		 * 
		 * @param n - ���ĸ���
		 * @param x - һά���飬����Ϊn����Ÿ�����n������ֵx(i)
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param dy - һά���飬����Ϊn������ʱ��dy(0)��Ÿ����������˵㴦��һ�׵���ֵ��
		 *             dy(n-1)��Ÿ���������Ҷ˵㴦��һ�׵���ֵ������ʱ�����n�������㴦��
		 *             һ�׵���ֵy'(i)��i=0,1,...,n-1
		 * @param ddy - һά���飬����Ϊn������ʱ�����n�������㴦�Ķ��׵���ֵy''(i)��
		 *              i=0,1,...,n-1
		 * @param m - ָ����ֵ��ĸ���
		 * @param t - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ���ֵ��
		 * @param z - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ�㴦�ĺ���ֵ
		 * @param dz - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ�㴦��һ�׵���ֵ
		 * @param ddz - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ�㴦�Ķ��׵���ֵ
		 * @return double �ͣ�ָ��������x(0)��x(n-1)�Ķ�����ֵ
		 */
		public static double GetValueSpline1(int n, double[] x, double[] y, double[] dy, double[] ddy, 
			int m, double[] t, double[] z, double[] dz, double[] ddz)
		{ 
			int i,j;
			double h0,h1,alpha,beta,g;
	    
			// ��ֵ
			double[] s=new double[n];
			s[0]=dy[0]; 
			dy[0]=0.0;
			h0=x[1]-x[0];
	    
			for (j=1;j<=n-2;j++)
			{ 
				h1=x[j+1]-x[j];
				alpha=h0/(h0+h1);
				beta=(1.0-alpha)*(y[j]-y[j-1])/h0;
				beta=3.0*(beta+alpha*(y[j+1]-y[j])/h1);
				dy[j]=-alpha/(2.0+(1.0-alpha)*dy[j-1]);
				s[j]=(beta-(1.0-alpha)*s[j-1]);
				s[j]=s[j]/(2.0+(1.0-alpha)*dy[j-1]);
				h0=h1;
			}
	    
			for (j=n-2;j>=0;j--)
				dy[j]=dy[j]*dy[j+1]+s[j];
	    
			for (j=0;j<=n-2;j++) 
				s[j]=x[j+1]-x[j];
	    
			for (j=0;j<=n-2;j++)
			{ 
				h1=s[j]*s[j];
				ddy[j]=6.0*(y[j+1]-y[j])/h1-2.0*(2.0*dy[j]+dy[j+1])/s[j];
			}
	    
			h1=s[n-2]*s[n-2];
			ddy[n-1]=6.0*(y[n-2]-y[n-1])/h1+2.0*(2.0*dy[n-1]+dy[n-2])/s[n-2];
			g=0.0;
	    
			for (i=0;i<=n-2;i++)
			{ 
				h1=0.5*s[i]*(y[i]+y[i+1]);
				h1=h1-s[i]*s[i]*s[i]*(ddy[i]+ddy[i+1])/24.0;
				g=g+h1;
			}
	    
			for (j=0;j<=m-1;j++)
			{ 
				if (t[j]>=x[n-1]) 
					i=n-2;
				else
				{ 
					i=0;
					while (t[j]>x[i+1]) 
						i=i+1;
				}
	        
				h1=(x[i+1]-t[j])/s[i];
				h0=h1*h1;
				z[j]=(3.0*h0-2.0*h0*h1)*y[i];
				z[j]=z[j]+s[i]*(h0-h0*h1)*dy[i];
				dz[j]=6.0*(h0-h1)*y[i]/s[i];
				dz[j]=dz[j]+(3.0*h0-2.0*h1)*dy[i];
				ddz[j]=(6.0-12.0*h1)*y[i]/(s[i]*s[i]);
				ddz[j]=ddz[j]+(2.0-6.0*h1)*dy[i]/s[i];
				h1=(t[j]-x[i])/s[i];
				h0=h1*h1;
				z[j]=z[j]+(3.0*h0-2.0*h0*h1)*y[i+1];
				z[j]=z[j]-s[i]*(h0-h0*h1)*dy[i+1];
				dz[j]=dz[j]-6.0*(h0-h1)*y[i+1]/s[i];
				dz[j]=dz[j]+(3.0*h0-2.0*h1)*dy[i+1];
				ddz[j]=ddz[j]+(6.0-12.0*h1)*y[i+1]/(s[i]*s[i]);
				ddz[j]=ddz[j]-(2.0-6.0*h1)*dy[i+1]/s[i];
			}
	    
			return(g);
		}

		/**
		 * �ڶ��ֱ߽���������������������ֵ��΢�������
		 * 
		 * @param n - ���ĸ���
		 * @param x - һά���飬����Ϊn����Ÿ�����n������ֵx(i)
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param dy - һά���飬����Ϊn������ʱ��dy(0)��Ÿ����������˵㴦��һ�׵���ֵ��
		 *             dy(n-1)��Ÿ���������Ҷ˵㴦��һ�׵���ֵ������ʱ�����n�������㴦��
		 *             һ�׵���ֵy'(i)��i=0,1,...,n-1
		 * @param ddy - һά���飬����Ϊn������ʱ�����n�������㴦�Ķ��׵���ֵy''(i)��
		 *              i=0,1,...,n-1
		 * @param m - ָ����ֵ��ĸ���
		 * @param t - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ���ֵ��
		 * @param z - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ�㴦�ĺ���ֵ
		 * @param dz - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ�㴦��һ�׵���ֵ
		 * @param ddz - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ�㴦�Ķ��׵���ֵ
		 * @return double �ͣ�ָ��������x(0)��x(n-1)�Ķ�����ֵ
		 */
		public static double GetValueSpline2(int n, double[] x, double[] y, double[] dy, double[] ddy, 
			int m, double[] t, double[] z, double[] dz, double[] ddz)
		{ 
			int i,j;
			double h0,h1=0,alpha,beta,g;
	    
			// ��ֵ
			double[] s=new double[n];
			dy[0]=-0.5;
			h0=x[1]-x[0];
			s[0]=3.0*(y[1]-y[0])/(2.0*h0)-ddy[0]*h0/4.0;
	    
			for (j=1;j<=n-2;j++)
			{ 
				h1=x[j+1]-x[j];
				alpha=h0/(h0+h1);
				beta=(1.0-alpha)*(y[j]-y[j-1])/h0;
				beta=3.0*(beta+alpha*(y[j+1]-y[j])/h1);
				dy[j]=-alpha/(2.0+(1.0-alpha)*dy[j-1]);
				s[j]=(beta-(1.0-alpha)*s[j-1]);
				s[j]=s[j]/(2.0+(1.0-alpha)*dy[j-1]);
				h0=h1;
			}
	    
			dy[n-1]=(3.0*(y[n-1]-y[n-2])/h1+ddy[n-1]*h1/2.0-s[n-2])/(2.0+dy[n-2]);
			for (j=n-2;j>=0;j--)
				dy[j]=dy[j]*dy[j+1]+s[j];
	    
			for (j=0;j<=n-2;j++) 
				s[j]=x[j+1]-x[j];
	    
			for (j=0;j<=n-2;j++)
			{ 
				h1=s[j]*s[j];
				ddy[j]=6.0*(y[j+1]-y[j])/h1-2.0*(2.0*dy[j]+dy[j+1])/s[j];
			}
	    
			h1=s[n-2]*s[n-2];
			ddy[n-1]=6.0*(y[n-2]-y[n-1])/h1+2.0*(2.0*dy[n-1]+dy[n-2])/s[n-2];
			g=0.0;
	    
			for (i=0;i<=n-2;i++)
			{ 
				h1=0.5*s[i]*(y[i]+y[i+1]);
				h1=h1-s[i]*s[i]*s[i]*(ddy[i]+ddy[i+1])/24.0;
				g=g+h1;
			}
	    
			for (j=0;j<=m-1;j++)
			{ 
				if (t[j]>=x[n-1]) 
					i=n-2;
				else
				{ 
					i=0;
					while (t[j]>x[i+1]) 
						i=i+1;
				}
	        
				h1=(x[i+1]-t[j])/s[i];
				h0=h1*h1;
				z[j]=(3.0*h0-2.0*h0*h1)*y[i];
				z[j]=z[j]+s[i]*(h0-h0*h1)*dy[i];
				dz[j]=6.0*(h0-h1)*y[i]/s[i];
				dz[j]=dz[j]+(3.0*h0-2.0*h1)*dy[i];
				ddz[j]=(6.0-12.0*h1)*y[i]/(s[i]*s[i]);
				ddz[j]=ddz[j]+(2.0-6.0*h1)*dy[i]/s[i];
				h1=(t[j]-x[i])/s[i];
				h0=h1*h1;
				z[j]=z[j]+(3.0*h0-2.0*h0*h1)*y[i+1];
				z[j]=z[j]-s[i]*(h0-h0*h1)*dy[i+1];
				dz[j]=dz[j]-6.0*(h0-h1)*y[i+1]/s[i];
				dz[j]=dz[j]+(3.0*h0-2.0*h1)*dy[i+1];
				ddz[j]=ddz[j]+(6.0-12.0*h1)*y[i+1]/(s[i]*s[i]);
				ddz[j]=ddz[j]-(2.0-6.0*h1)*dy[i+1]/s[i];
			}
	    
			return(g);
		}

		/**
		 * �����ֱ߽���������������������ֵ��΢�������
		 * 
		 * @param n - ���ĸ���
		 * @param x - һά���飬����Ϊn����Ÿ�����n������ֵx(i)
		 * @param y - һά���飬����Ϊn����Ÿ�����n�����ĺ���ֵy(i)��
		 *            y(i) = f(x(i)), i=0,1,...,n-1
		 * @param dy - һά���飬����Ϊn������ʱ��dy(0)��Ÿ����������˵㴦��һ�׵���ֵ��
		 *             dy(n-1)��Ÿ���������Ҷ˵㴦��һ�׵���ֵ������ʱ�����n�������㴦��
		 *             һ�׵���ֵy'(i)��i=0,1,...,n-1
		 * @param ddy - һά���飬����Ϊn������ʱ�����n�������㴦�Ķ��׵���ֵy''(i)��
		 *              i=0,1,...,n-1
		 * @param m - ָ����ֵ��ĸ���
		 * @param t - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ���ֵ��
		 * @param z - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ�㴦�ĺ���ֵ
		 * @param dz - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ�㴦��һ�׵���ֵ
		 * @param ddz - һά���飬����Ϊm�����m��ָ���Ĳ�ֵ�㴦�Ķ��׵���ֵ
		 * @return double �ͣ�ָ��������x(0)��x(n-1)�Ķ�����ֵ
		 */
		public static double GetValueSpline3(int n, double[] x, double[] y, double[] dy, double[] ddy, 
			int m, double[] t, double[] z, double[] dz, double[] ddz)
		{ 
			int i,j;
			double h0,y0,h1,y1,alpha=0,beta=0,u,g;
	    
			// ��ֵ
			double[] s=new double[n];
			h0=x[n-1]-x[n-2];
			y0=y[n-1]-y[n-2];
			dy[0]=0.0; ddy[0]=0.0; ddy[n-1]=0.0;
			s[0]=1.0; s[n-1]=1.0;

			for (j=1;j<=n-1;j++)
			{ 
				h1=h0; y1=y0;
				h0=x[j]-x[j-1];
				y0=y[j]-y[j-1];
				alpha=h1/(h1+h0);
				beta=3.0*((1.0-alpha)*y1/h1+alpha*y0/h0);
	        
				if (j<n-1)
				{ 
					u=2.0+(1.0-alpha)*dy[j-1];
					dy[j]=-alpha/u;
					s[j]=(alpha-1.0)*s[j-1]/u;
					ddy[j]=(beta-(1.0-alpha)*ddy[j-1])/u;
				}
			}
	    
			for (j=n-2;j>=1;j--)
			{ 
				s[j]=dy[j]*s[j+1]+s[j];
				ddy[j]=dy[j]*ddy[j+1]+ddy[j];
			}
	    
			dy[n-2]=(beta-alpha*ddy[1]-(1.0-alpha)*ddy[n-2])/
				(alpha*s[1]+(1.0-alpha)*s[n-2]+2.0);
	    
			for (j=2;j<=n-1;j++)
				dy[j-2]=s[j-1]*dy[n-2]+ddy[j-1];
	    
			dy[n-1]=dy[0];
			for (j=0;j<=n-2;j++) 
				s[j]=x[j+1]-x[j];
	    
			for (j=0;j<=n-2;j++)
			{ 
				h1=s[j]*s[j];
				ddy[j]=6.0*(y[j+1]-y[j])/h1-2.0*(2.0*dy[j]+dy[j+1])/s[j];
			}
	    
			h1=s[n-2]*s[n-2];
			ddy[n-1]=6.0*(y[n-2]-y[n-1])/h1+2.0*(2.0*dy[n-1]+dy[n-2])/s[n-2];
			g=0.0;
	    
			for (i=0;i<=n-2;i++)
			{ 
				h1=0.5*s[i]*(y[i]+y[i+1]);
				h1=h1-s[i]*s[i]*s[i]*(ddy[i]+ddy[i+1])/24.0;
				g=g+h1;
			}
	    
			for (j=0;j<=m-1;j++)
			{ 
				h0=t[j];
				while (h0>=x[n-1]) 
					h0=h0-(x[n-1]-x[0]);
	        
				while (h0<x[0]) 
					h0=h0+(x[n-1]-x[0]);
	        
				i=0;
				while (h0>x[i+1]) 
					i=i+1;
	        
				u=h0;
				h1=(x[i+1]-u)/s[i];
				h0=h1*h1;
				z[j]=(3.0*h0-2.0*h0*h1)*y[i];
				z[j]=z[j]+s[i]*(h0-h0*h1)*dy[i];
				dz[j]=6.0*(h0-h1)*y[i]/s[i];
				dz[j]=dz[j]+(3.0*h0-2.0*h1)*dy[i];
				ddz[j]=(6.0-12.0*h1)*y[i]/(s[i]*s[i]);
				ddz[j]=ddz[j]+(2.0-6.0*h1)*dy[i]/s[i];
				h1=(u-x[i])/s[i];
				h0=h1*h1;
				z[j]=z[j]+(3.0*h0-2.0*h0*h1)*y[i+1];
				z[j]=z[j]-s[i]*(h0-h0*h1)*dy[i+1];
				dz[j]=dz[j]-6.0*(h0-h1)*y[i+1]/s[i];
				dz[j]=dz[j]+(3.0*h0-2.0*h1)*dy[i+1];
				ddz[j]=ddz[j]+(6.0-12.0*h1)*y[i+1]/(s[i]*s[i]);
				ddz[j]=ddz[j]-(2.0-6.0*h1)*dy[i+1]/s[i];
			}
	 
			return(g);
		}

		/**
		 * ��Ԫ�����ֵ
		 * 
		 * @param n - x�����ϸ������ĵ���
		 * @param x - һά���飬����Ϊn����Ÿ���n x m �����x�����ϵ�n��ֵx(i)
		 * @param m - y�����ϸ������ĵ���
		 * @param y - һά���飬����Ϊm����Ÿ���n x m �����y�����ϵ�m��ֵy(i)
		 * @param z - һά���飬����Ϊn x m����Ÿ�����n x m�����ĺ���ֵz(i,j)��
		 *            z(i,j) = f(x(i), y(j)), i=0,1,...,n-1, j=0,1,...,m-1
		 * @param u - ��Ų�ֵ��x����
		 * @param v - ��Ų�ֵ��y����
		 * @return double �ͣ�ָ������ֵf(u, v)
		 */
		public static double GetValueTqip(int n, double[] x, int m, double[] y, double[] z, double u, double v)
		{ 
			int nn,mm,ip,iq,i,j,k,l;
			double[] b = new double[3];
			double h,w;
	    
			// ��ֵ
			nn=3;

			// ����
			if (n<=3) 
			{ 
				ip=0;  
				nn=n;
			}
			else if (u<=x[1]) 
				ip=0;
			else if (u>=x[n-2]) 
				ip=n-3;
			else					
			{ 
				i=1; j=n;
				while (((i-j)!=1)&&((i-j)!=-1))
				{ 
					l=(i+j)/2;
					if (u<x[l-1]) 
						j=l;
					else 
						i=l;
				}
	        
				if (Math.Abs(u-x[i-1])<Math.Abs(u-x[j-1])) 
					ip=i-2;
				else 
					ip=i-1;
			}
	    
			mm=3;
	    
			if (m<=3) 
			{ 
				iq=0; 
				mm=m;
			}
			else if (v<=y[1]) 
				iq=0;
			else if (v>=y[m-2]) 
				iq=m-3;
			else
			{ 
				i=1; 
				j=m;
				while (((i-j)!=1)&&((i-j)!=-1))
				{ 
					l=(i+j)/2;
					if (v<y[l-1]) 
						j=l;
					else 
						i=l;
				}
	        
				if (Math.Abs(v-y[i-1])<Math.Abs(v-y[j-1])) 
					iq=i-2;
				else 
					iq=i-1;
			}
	    
			for (i=0;i<=nn-1;i++)
			{ 
				b[i]=0.0;
				for (j=0;j<=mm-1;j++)
				{ 
					k=m*(ip+i)+(iq+j);
					h=z[k];
					for (k=0;k<=mm-1;k++)
					{
						if (k!=j)
							h=h*(v-y[iq+k])/(y[iq+j]-y[iq+k]);
					}

					b[i]=b[i]+h;
				}
			}
	    
			w=0.0;
			for (i=0;i<=nn-1;i++)
			{ 
				h=b[i];
				for (j=0;j<=nn-1;j++)
				{
					if (j!=i)
						h=h*(u-x[ip+j])/(x[ip+i]-x[ip+j]);
				}

				w=w+h;
			}
	    
			return(w);
		}

		/**
		 * ��Ԫȫ�����ֵ
		 * 
		 * @param n - x�����ϸ������ĵ���
		 * @param x - һά���飬����Ϊn����Ÿ���n x m �����x�����ϵ�n��ֵx(i)
		 * @param m - y�����ϸ������ĵ���
		 * @param y - һά���飬����Ϊm����Ÿ���n x m �����y�����ϵ�m��ֵy(i)
		 * @param z - һά���飬����Ϊn x m����Ÿ�����n x m�����ĺ���ֵz(i,j)��
		 *            z(i,j) = f(x(i), y(j)), i=0,1,...,n-1, j=0,1,...,m-1
		 * @param u - ��Ų�ֵ��x����
		 * @param v - ��Ų�ֵ��y����
		 * @return double �ͣ�ָ������ֵf(u, v)
		 */
		public static double GetValueLagrange2(int n, double[] x, int m, double[] y, double[] z, double u, double v)
		{ 
			int ip,ipp,i,j,l,iq,iqq,k;
			double h,w;
			double[] b = new double[10];
	    
			// ����
			if (u<=x[0]) 
			{ 
				ip=1; 
				ipp=4;
			}
			else if (u>=x[n-1]) 
			{ 
				ip=n-3; 
				ipp=n;
			}
			else
			{ 
				i=1; 
				j=n;
				while (((i-j)!=1)&&((i-j)!=-1))
				{ 
					l=(i+j)/2;
					if (u<x[l-1]) 
						j=l;
					else 
						i=l;
				}
	        
				ip=i-3; 
				ipp=i+4;
			}
	    
			if (ip<1) 
				ip=1;

			if (ipp>n) 
				ipp=n;

			if (v<=y[0]) 
			{ 
				iq=1; 
				iqq=4;
			}
			else if (v>=y[m-1]) 
			{ 
				iq=m-3; 
				iqq=m;
			}
			else
			{ 
				i=1; 
				j=m;
				while (((i-j)!=1)&&((i-j)!=-1))
				{ 
					l=(i+j)/2;
					if (v<y[l-1]) 
						j=l;
					else 
						i=l;
				}
	        
				iq=i-3; 
				iqq=i+4;
			}
	    
			if (iq<1) 
				iq=1;

			if (iqq>m) 
				iqq=m;

			for (i=ip-1;i<=ipp-1;i++)
			{ 
				b[i-ip+1]=0.0;
				for (j=iq-1;j<=iqq-1;j++)
				{ 
					h=z[m*i+j];
					for (k=iq-1;k<=iqq-1;k++)
					{
						if (k!=j) 
							h=h*(v-y[k])/(y[j]-y[k]);
					}

					b[i-ip+1]=b[i-ip+1]+h;
				}
			}
	    
			w=0.0;
			for (i=ip-1;i<=ipp-1;i++)
			{ 
				h=b[i-ip+1];
				for (j=ip-1;j<=ipp-1;j++)
				{
					if (j!=i) 
						h=h*(u-x[j])/(x[i]-x[j]);
				}

				w=w+h;
			}
	    
			return(w);
		}
	}
}