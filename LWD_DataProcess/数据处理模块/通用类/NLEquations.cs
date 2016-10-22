/*
 * �������Է�������� NLEquations
 * 
 */

using System;

namespace LWD_DataProcess
{
	/**
	 * �������Է�������� NLEquations
	 *
	 * @version 1.0
	 */
	public abstract class NLEquations 
	{
		/**
		 * �麯�������㷽����˺���ֵ���������������и��Ǹ��ຯ��
		 * 
		 * @param x - ����
		 * @return ����ֵ
		 */
		protected virtual double Func(double x)
		{
			return 0;
		}
	
		/**
		 * �麯�������㷽����˺���ֵ���������������и��Ǹ��ຯ��
		 * 
		 * @param x - ����ֵ����
		 * @return ����ֵ
		 */
		protected virtual double Func(double[] x)
		{
			return 0;
		}


		/**
		 * �麯�������㷽����˺���ֵ���������������и��Ǹ��ຯ��
		 * 
		 * @param x - ����
		 * @param y - ����ֵ����
		 */
		protected virtual void Func(double x, double[] y)
		{
		}
	
		/**
		 * �麯�������㷽����˺���ֵ���������������и��Ǹ��ຯ��
		 * 
		 * @param x - ��Ԫ�����ı���
		 * @param y - ��Ԫ�����ı���
		 * @return ����ֵ
		 */
		protected virtual double Func(double x, double y)
		{
			return 0;
		}
	
		/**
		 * �麯�������㷽����˺���ֵ���������������и��Ǹ��ຯ��
		 * 
		 * @param x - ��Ԫ�����ı���ֵ����
		 * @param y - ��Ԫ�����ı���ֵ����
		 * @return ����ֵ
		 */
		protected virtual double Func(double[] x, double[] y)
		{
			return 0;
		}
	
		/**
		 * �麯�������㷽����˺���ֵ���������������и��Ǹ��ຯ��
		 * 
		 * @param x - ��֪����ֵ����
		 * @param p - ��֪����ֵ����
		 */
		protected virtual void FuncMJ(double[] x, double[] p)
		{
		}

		/**
		 * �������캯��
		 */
		public NLEquations()
		{
		}

		/**
		 * ������Է���ʵ���ĶԷַ�
		 * 
		 * ����ʱ���븲�Ǽ��㷽����˺���f(x)ֵ���麯��
		 *        double Func(double x)
		 * 
		 * @param nNumRoots - ��[xStart, xEnd]��ʵ��������Ԥ��ֵ
		 * @param x - һά���飬����Ϊm������������[xStart, xEnd]����������ʵ����
		 *            ʵ�������ɺ���ֵ����
		 * @param xStart - ����������˵�
		 * @param xEnd - ���������Ҷ˵�
		 * @param dblStep - �������ʱ���õĲ���
		 * @param eps - ���ȿ��Ʋ���
		 * @return int �ͣ���õ�ʵ������Ŀ
		 */
		public int GetRootBisect(int nNumRoots, double[] x, double xStart, double xEnd, double dblStep, double eps)
		{
			int n,js;
			double z,y,z1,y1,z0,y0;
	
			// ���ĸ�����0
			n = 0; 
	
			// ����˵㿪ʼ����
			z = xStart; 
			y = Func(z);
	
			// ѭ�����
			while ((z<=xEnd+dblStep/2.0)&&(n!=nNumRoots))
			{ 
				if (Math.Abs(y)<eps)
				{ 
					n=n+1; 
					x[n-1]=z;
					z=z+dblStep/2.0; 
					y=Func(z);
				}
				else
				{ 
					z1=z+dblStep; 
					y1=Func(z1);
	             
					if (Math.Abs(y1)<eps)
					{ 
						n=n+1; 
						x[n-1]=z1;
						z=z1+dblStep/2.0; 
						y=Func(z);
					}
					else if (y*y1>0.0)
					{ 
						y=y1; 
						z=z1;
					}
					else
					{ 
						js=0;
						while (js==0)
						{ 
							if (Math.Abs(z1-z)<eps)
							{ 
								n=n+1; 
								x[n-1]=(z1+z)/2.0;
								z=z1+dblStep/2.0; y=Func(z);
								js=1;
							}
							else
							{ 
								z0=(z1+z)/2.0; 
								y0=Func(z0);
								if (Math.Abs(y0)<eps)
								{ 
									x[n]=z0; 
									n=n+1; 
									js=1;
									z=z0+dblStep/2.0; 
									y=Func(z);
								}
								else if ((y*y0)<0.0)
								{ 
									z1=z0; 
									y1=y0;
								}
								else 
								{ 
									z=z0; 
									y=y0;
								}
							}
						}
					}
				}
			}
	     
			// ����ʵ������Ŀ
			return(n);
		}
	
		/**
		 * ������Է���һ��ʵ����ţ�ٷ�
		 * 
		 * ����ʱ���븲�Ǽ��㷽����˺���f(x)����һ�׵���f'(x)ֵ���麯��:
		 * 	void Func(double x, double[] y)
		 * 	y(0) ����f(x)��ֵ
		 * 	y(1) ����f'(x)��ֵ
		 * 
		 * @param x - ���������ֵ���²�⣩��������������õ�һ��ʵ��
		 * @param nMaxIt - �ݹ����
		 * @param eps - ���ȿ��Ʋ���
		 * @return bool �ͣ�����Ƿ�ɹ�
		 */
		public bool GetRootNewton(ref double x, int nMaxIt, double eps)
		{ 
			int l;
			double d,p,x0,x1=0.0;
			double[] y = new double[2];
	
			// ����ֵ
			l=nMaxIt; 
			x0=x;
			Func(x0,y);
	     
			// ��⣬���ƾ���
			d=eps+1.0;
			while ((d>=eps)&&(l!=0))
			{ 
				if (y[1] == 0.0)
					return false;
	
				x1=x0-y[0]/y[1];
				Func(x1,y);
	         
				d=Math.Abs(x1-x0); 
				p=Math.Abs(y[0]);
				if (p>d) 
					d=p;
				x0=x1; 
				l=l-1;
			}
	     
			x = x1;
	
			return true;
		}
	
		/**
		 * ������Է���һ��ʵ���İ��ؽ������
		 * 
		 * ����ʱ���븲�Ǽ��㷽����˺���f(x)ֵ���麯��
		 *       double Func(double x)
		 * 
		 * @param x - ���������ֵ���²�⣩��������������õ�һ��ʵ��
		 * @param nMaxIt - �ݹ����
		 * @param eps - ���ȿ��Ʋ���
		 * @return bool �ͣ�����Ƿ�ɹ�
		 */
		public bool GetRootAitken(ref double x, int nMaxIt, double eps)
		{ 
			int flag,l;
			double u,v,x0;
	
			// �������
			l=0; 
			x0=x; 
			flag=0;
	
			// �������
			while ((flag==0)&&(l!=nMaxIt))
			{ 
				l=l+1; 
				u=Func(x0); 
				v=Func(u);
				if (Math.Abs(u-v)<eps) 
				{ 
					x0=v; 
					flag=1; 
				}
				else 
					x0=v-(v-u)*(v-u)/(v-2.0*u+x0);
			}
	     
			x = x0; 
	     
			// �Ƿ���ָ���ĵ��������ڴﵽ��⾫��
			return (nMaxIt > l);
		}
	
		/**
		 * ������Է���һ��ʵ��������ʽ�ⷨ
		 * 
		 * ����ʱ���븲�Ǽ��㷽����˺���f(x)ֵ���麯��
		 *       double Func(double x)
		 * 
		 * @param x - ���������ֵ���²�⣩��������������õ�һ��ʵ��
		 * @param eps - ���ȿ��Ʋ���
		 * @return bool �ͣ�����Ƿ�ɹ�
		 */
		public bool GetRootPq(ref double x, double eps)
		{ 
			int i,j,m,it=0,l;
			double z,h,x0,q;
			double[] a = new double[10];
			double[] y = new double[10];
	
			// �������
			l=10; 
			q=1.0e+35; 
			x0=x; 
			h=0.0;
	     
			// ����ʽ���
			while (l!=0)
			{ 
				l=l-1; 
				j=0; 
				it=l;
				while (j<=7)
				{ 
					if (j<=2) 
						z=x0+0.1*j;
					else 
						z=h;
	 			
					y[j]=Func(z);
					h=z;
					if (j==0) 
						a[0]=z;
					else
					{ 
						m=0; 
						i=0;
						while ((m==0)&&(i<=j-1))
						{ 
							if (Math.Abs(h-a[i])+1.0==1.0) 
								m=1;
							else 
								h=(y[j]-y[i])/(h-a[i]);
	                      
							i=i+1;
						}
						a[j]=h;
						if (m!=0) 
							a[j]=q;
						h=0.0;
						for (i=j-1; i>=0; i--)
						{ 
							if (Math.Abs(a[i+1]+h)+1.0==1.0) 
								h=q;
							else 
								h=-y[i]/(a[i+1]+h);
						}
	                  
						h=h+a[0];
					}
	              
					if (Math.Abs(y[j])>=eps) 
						j=j+1;
					else 
					{ 
						j=10; 
						l=0;
					}
				}
	         
				x0=h;
			}
	
			x = h;
	     
			// �Ƿ���10������ʽ�����ʵ����
			return (10>it);
		}
	
		/**
		 * ��ʵϵ����������ȫ������QR����
		 * 
		 * @param n - ����ʽ���̵Ĵ���
		 * @param dblCoef - һά���飬����Ϊn+1�������ݴ������δ��n�ζ���ʽ���̵�
		 *                  n+1��ϵ��
		 * @param xr - һά���飬����Ϊn������n������ʵ��
		 * @param xi - һά���飬����Ϊn������n�������鲿
		 * @param nMaxIt - ��������
		 * @param eps - ���ȿ��Ʋ���
		 * @return bool �ͣ�����Ƿ�ɹ�
		 */
		public bool GetRootQr(int n, double[] dblCoef, double[] xr, double[] xi, int nMaxIt, double eps)
		{ 
			// ��ʼ������
			Matrix mtxQ = new Matrix();
			mtxQ.Init(n, n);
			double[] q = mtxQ.GetData();
	
			// ������겮�����
			for (int j=0; j<=n-1; j++)
				q[j]=-dblCoef[n-j-1]/dblCoef[n];
	
			for (int j=n; j<=n*n-1; j++)
				q[j]=0.0;
	
			for (int i=0; i<=n-2; i++)
				q[(i+1)*n+i]=1.0;
	
			// ����겮����������ֵ��������������Ϊ���̵Ľ�
			if (mtxQ.ComputeEvHBerg(xr, xi, nMaxIt, eps))
				return true;
	
			return false;
		}
	
		/**
		 * ��ʵϵ����������ȫ������ţ����ɽ��
		 * 
		 * @param n - ����ʽ���̵Ĵ���
		 * @param dblCoef - һά���飬����Ϊn+1�������ݴ������δ��n�ζ���ʽ���̵�
		 *                  n+1��ϵ��
		 * @param xr - һά���飬����Ϊn������n������ʵ��
		 * @param xi - һά���飬����Ϊn������n�������鲿
		 * @return bool �ͣ�����Ƿ�ɹ�
		 */
		public bool GetRootNewtonDownHill(int n, double[] dblCoef, double[] xr, double[] xi)
		{ 
			int m=0,i=0,jt=0,k=0,nis=0,it=0;
			double t=0,x=0,y=0,x1=0,y1=0,dx=0,dy=0,p=0,q=0,w=0,dd=0,dc=0,c=0;
			double g=0,u=0,v=0,pq=0,g1=0,u1=0,v1=0;
	     
			// ��ʼ�ж�
			m=n;
			while ((m>0)&&(Math.Abs(dblCoef[m])+1.0==1.0)) 
				m=m-1;
	
			// ���ʧ��
			if (m<=0)
				return false;
	
			for (i=0; i<=m; i++)
				dblCoef[i]=dblCoef[i]/dblCoef[m];
	     
			for (i=0; i<=m/2; i++)
			{ 
				w=dblCoef[i]; 
				dblCoef[i]=dblCoef[m-i]; 
				dblCoef[m-i]=w;
			}
	     
			// �������
			k=m; 
			nis=0; 
			w=1.0;
			jt=1;
			while (jt==1)
			{ 
				pq=Math.Abs(dblCoef[k]);
				while (pq<1.0e-12)
				{ 
					xr[k-1]=0.0; 
					xi[k-1]=0.0; 
					k=k-1;
					if (k==1)
					{ 
						xr[0]=-dblCoef[1]*w/dblCoef[0]; 
						xi[0]=0.0;
	                 
						return true;
					}
	             
					pq=Math.Abs(dblCoef[k]);
				}
	 	
				q=Math.Log(pq); 
				q=q/(1.0*k); 
				q=Math.Exp(q);
				p=q; 
				w=w*p;
				for (i=1; i<=k; i++)
				{ 
					dblCoef[i]=dblCoef[i]/q; 
					q=q*p;
				}
	         
				x=0.0001; 
				x1=x; 
				y=0.2; 
				y1=y; 
				dx=1.0;
				g=1.0e+37;
	        
				while (true)
				{
					u=dblCoef[0]; v=0.0;
					for (i=1; i<=k; i++)
					{ 
						p=u*x1; 
						q=v*y1;
						pq=(u+v)*(x1+y1);
						u=p-q+dblCoef[i]; 
						v=pq-p-q;
					}
		         
					g1=u*u+v*v;
					if (g1>=g)
					{ 
						if (nis!=0)
						{ 
							it=1;
							g65(ref x, ref y, ref x1, ref y1, ref dx, ref dy, ref dd, ref dc, ref c, ref k, ref nis, ref it);
						 
							if (it==0) 
								continue;
						}
						else
						{ 
							g60(ref t,ref x,ref y,ref x1,ref y1,ref dx,ref dy,ref p,ref q,ref k,ref it);
		 				 
							if (t>=1.0e-03) 
								continue;
		                 
							if (g>1.0e-18)
							{ 
								it=0;
								g65(ref x, ref y, ref x1, ref y1, ref dx, ref dy, ref dd, ref dc, ref c, ref k, ref nis, ref it);

								if (it==0) 
									continue;
							}
						}
		             
						g90(xr,xi,dblCoef,ref x,ref y,ref p,ref q,ref w,ref k);
						break;
					}
					else
					{ 
						g=g1; 
						x=x1; 
						y=y1; 
						nis=0;
						if (g<=1.0e-22)
						{
							g90(xr,xi,dblCoef,ref x,ref y,ref p,ref q,ref w,ref k);
						}
						else
						{ 
							u1=k*dblCoef[0]; 
							v1=0.0;
							for (i=2; i<=k; i++)
							{ 
								p=u1*x; 
								q=v1*y; 
								pq=(u1+v1)*(x+y);
								u1=p-q+(k-i+1)*dblCoef[i-1];
								v1=pq-p-q;
							}
		                 
							p=u1*u1+v1*v1;
							if (p<=1.0e-20)
							{ 
								it=0;
								g65(ref x, ref y, ref x1, ref y1, ref dx, ref dy, ref dd, ref dc, ref c, ref k, ref nis, ref it);

								if (it==0) 
									continue;
		
								g90(xr,xi,dblCoef,ref x,ref y,ref p,ref q,ref w,ref k);
							}
							else
							{ 
								dx=(u*u1+v*v1)/p;
								dy=(u1*v-v1*u)/p;
								t=1.0+4.0/k;
								
								g60(ref t,ref x,ref y,ref x1,ref y1,ref dx,ref dy,ref p,ref q,ref k,ref it);

								if (t>=1.0e-03) 
									continue;
		
								if (g>1.0e-18)
								{ 
									it=0;

									g65(ref x, ref y, ref x1, ref y1, ref dx, ref dy, ref dd, ref dc, ref c, ref k, ref nis, ref it);

									if (it==0) 
										continue;
								}
		                     
								g90(xr,xi,dblCoef,ref x,ref y,ref p,ref q,ref w,ref k);
							}
						}
						break;
					}
				}
	        
				if (k==1) 
					jt=0;
				else 
					jt=1;
			}
	     
			return true;
		}
	
	
		/**
		 * �ڲ�����
		 */
		private void g60(ref double t,ref double x,ref double y,ref double x1,ref double y1,ref double dx,ref double dy,ref double p,ref double q,ref int k,ref int it)
		{ 
			it=1;
			while (it==1)
			{ 
				t=t/1.67; 
				it=0;
				x1=x-(t)*(dx);
				y1=y-(t)*(dy);
				if (k>=50)
				{ 
					p=Math.Sqrt((x1)*(x1)+(y1)*(y1));
					q=Math.Exp(85.0/(k));
					if (p>=q) 
						it=1;
				}
			}
		}
	
		/**
		 * �ڲ�����
		 */
		private void g90(double[] xr,double[] xi,double[] dblCoef,ref double x,ref double y,ref double p,ref double q,ref double w,ref int k)
		{ 
			int i;
    
			if (Math.Abs(y)<=1.0e-06)
			{ 
				p=-(x); 
				y=0.0; 
				q=0.0;
			}
			else
			{ 
				p=-2.0*(x); 
				q=(x)*(x)+(y)*(y);
				xr[k-1]=(x)*(w);
				xi[k-1]=-(y)*(w);
				k=k-1;
			}
    
			for (i=1; i<=k; i++)
			{ 
				dblCoef[i]=dblCoef[i]-dblCoef[i-1]*(p);
				dblCoef[i+1]=dblCoef[i+1]-dblCoef[i-1]*(q);
			}
    
			xr[k-1]=(x)*(w); 
			xi[k-1]=(y)*(w);
			k=k-1;
			if (k==1)
			{ 
				xr[0]=-dblCoef[1]*(w)/dblCoef[0]; 
				xi[0]=0.0;
			}
		}
	
		/**
		 * �ڲ�����
		 */
		private void g65(ref double x,ref double y,ref double x1,ref double y1,ref double dx,ref double dy,ref double dd,ref double dc,ref double c,ref int k,ref int nis,ref int it)
		{ 
			if (it==0)
			{ 
				nis=1;
				dd=Math.Sqrt((dx)*(dx)+(dy)*(dy));
				if (dd>1.0) 
					dd=1.0;
				dc=6.28/(4.5*(k)); 
				c=0.0;
			}
    
			while(true)
			{ 
				c=c+(dc);
				dx=(dd)*Math.Cos(c); 
				dy=(dd)*Math.Sin(c);
				x1=x+dx; 
				y1=y+dy;
				if (c<=6.29)
				{ 
					it=0; 
					return;
				}
        
				dd=dd/1.67;
				if (dd<=1.0e-07)
				{ 
					it=1; 
					return;
				}
        
				c=0.0;
			}
		}
	
		/**
		 * ��ϵ����������ȫ������ţ����ɽ��
		 * 
		 * @param n - ����ʽ���̵Ĵ���
		 * @param ar - һά���飬����Ϊn+1�������ݴ������δ��n�ζ���ʽ���̵�
		 *             n+1��ϵ����ʵ��
		 * @param ai - һά���飬����Ϊn+1�������ݴ������δ��n�ζ���ʽ���̵�
		 *             n+1��ϵ�����鲿
		 * @param xr - һά���飬����Ϊn������n������ʵ��
		 * @param xi - һά���飬����Ϊn������n�������鲿
		 * @return bool �ͣ�����Ƿ�ɹ�
		 */
		public bool GetRootNewtonDownHill(int n, double[] ar, double[] ai, double[] xr, double[] xi)
		{ 
			int m=0,i=0,jt=0,k=0,nis=0,it=0;
			double t=0,x=0,y=0,x1=0,y1=0,dx=0,dy=0,p=0,q=0,w=0,dd=0,dc=0,c=0;
			double g=0,u=0,v=0,pq=0,g1=0,u1=0,v1=0;
	
			// ��ʼ�ж�
			m=n;
			p=Math.Sqrt(ar[m]*ar[m]+ai[m]*ai[m]);
			while ((m>0)&&(p+1.0==1.0))
			{  
				m=m-1;
				p=Math.Sqrt(ar[m]*ar[m]+ai[m]*ai[m]);
			}
	     
			// ���ʧ��
			if (m<=0)
				return false;
	
			for (i=0; i<=m; i++)
			{ 
				ar[i]=ar[i]/p; 
				ai[i]=ai[i]/p;
			}
	     
			for (i=0; i<=m/2; i++)
			{ 
				w=ar[i]; 
				ar[i]=ar[m-i]; 
				ar[m-i]=w;
				w=ai[i]; 
				ai[i]=ai[m-i]; 
				ai[m-i]=w;
			}
	     
			// �������
			k=m; 
			nis=0; 
			w=1.0;
			jt=1;
			while (jt==1)
			{ 
				pq=Math.Sqrt(ar[k]*ar[k]+ai[k]*ai[k]);
				while (pq<1.0e-12)
				{ 
					xr[k-1]=0.0; 
					xi[k-1]=0.0; 
					k=k-1;
					if (k==1)
					{ 
						p=ar[0]*ar[0]+ai[0]*ai[0];
						xr[0]=-w*(ar[0]*ar[1]+ai[0]*ai[1])/p;
						xi[0]=w*(ar[1]*ai[0]-ar[0]*ai[1])/p;
	                 
						return true;
					}
	             
					pq=Math.Sqrt(ar[k]*ar[k]+ai[k]*ai[k]);
				}
	 		
				q=Math.Log(pq); 
				q=q/(1.0*k); 
				q=Math.Exp(q);
				p=q; 
				w=w*p;
				for (i=1; i<=k; i++)
				{ 
					ar[i]=ar[i]/q; 
					ai[i]=ai[i]/q; 
					q=q*p;
				}
	         
				x=0.0001; 
				x1=x; 
				y=0.2; 
				y1=y; 
				dx=1.0;
				g=1.0e+37; 
	 
				while (true)
				{
					u=ar[0]; 
					v=ai[0];
					for (i=1; i<=k; i++)
					{ 
						p=u*x1; 
						q=v*y1;
						pq=(u+v)*(x1+y1);
						u=p-q+ar[i]; 
						v=pq-p-q+ai[i];
					}
		         
					g1=u*u+v*v;
					if (g1>=g)
					{ 
						if (nis!=0)
						{ 
							it=1;
							g65c(ref x, ref y, ref x1, ref y1, ref dx, ref dy, ref dd, ref dc, ref c, ref k, ref nis, ref it);
							if (it==0) 
								continue;
						}
						else
						{ 
							g60c(ref t,ref x,ref y,ref x1,ref y1,ref dx,ref dy,ref p,ref q,ref k,ref it);
							if (t>=1.0e-03) 
								continue;
		                 
							if (g>1.0e-18)
							{ 
								it=0;
								g65c(ref x, ref y, ref x1, ref y1, ref dx, ref dy, ref dd, ref dc, ref c, ref k, ref nis, ref it);
								if (it==0) 
									continue;
							}
						}
		             
						g90c(xr,xi,ar,ai,ref x,ref y,ref p,ref w,ref k);
						break;
					}
					else
					{ 
						g=g1; 
						x=x1; 
						y=y1; 
						nis=0;
						if (g<=1.0e-22)
						{
							g90c(xr,xi,ar,ai,ref x,ref y,ref p,ref w,ref k);
						}
						else
						{ 
							u1=k*ar[0]; 
							v1=ai[0];
							for (i=2; i<=k; i++)
							{ 
								p=u1*x; 
								q=v1*y; 
								pq=(u1+v1)*(x+y);
								u1=p-q+(k-i+1)*ar[i-1];
								v1=pq-p-q+(k-i+1)*ai[i-1];
							}
		                 
							p=u1*u1+v1*v1;
							if (p<=1.0e-20)
							{ 
								it=0;
								g65c(ref x, ref y, ref x1, ref y1, ref dx, ref dy, ref dd, ref dc, ref c, ref k, ref nis, ref it);
								if (it==0) 
									continue;
		                     
								g90c(xr,xi,ar,ai,ref x,ref y,ref p,ref w,ref k);
							}
							else
							{ 
								dx=(u*u1+v*v1)/p;
								dy=(u1*v-v1*u)/p;
								t=1.0+4.0/k;
								g60c(ref t,ref x,ref y,ref x1,ref y1,ref dx,ref dy,ref p,ref q,ref k,ref it);
								if (t>=1.0e-03) 
									continue;
		                     
								if (g>1.0e-18)
								{ 
									it=0;
									g65c(ref x, ref y, ref x1, ref y1, ref dx, ref dy, ref dd, ref dc, ref c, ref k, ref nis, ref it);
									if (it==0) 
										continue;
								}
		                     
								g90c(xr,xi,ar,ai,ref x,ref y,ref p,ref w,ref k);
							}
						}
						break;
					}
				}
	         
				if (k==1) 
					jt=0;
				else 
					jt=1;
			}
	     
			return true;
		}
	
		/**
		 * �ڲ�����
		 */
		private void g60c(ref double t,ref double x,ref double y,ref double x1,ref double y1,ref double dx,ref double dy,ref double p,ref double q,ref int k,ref int it)
		{ 
			it=1;
			while (it==1)
			{ 
				t=t/1.67; 
				it=0;
				x1=x-t*(dx);
				y1=y-t*(dy);
				if (k>=30)
				{ 
					p=Math.Sqrt(x1*(x1)+y1*(y1));
					q=Math.Exp(75.0/(k));
					if (p>=q) 
						it=1;
				}
			}
		}

	
		/**
		 * �ڲ�����
		 */
		private void g90c(double[] xr,double[] xi,double[] ar,double[] ai,ref double x,ref double y,ref double p,ref double w,ref int k)
		{ 
			int i;
			for (i=1; i<=k; i++)
			{ 
				ar[i]=ar[i]+ar[i-1]*(x)-ai[i-1]*(y);
				ai[i]=ai[i]+ar[i-1]*(y)+ai[i-1]*(x);
			}
    
			xr[k-1]=x*(w); 
			xi[k-1]=y*(w);
			k=k-1;
			if (k==1)
			{ 
				p=ar[0]*ar[0]+ai[0]*ai[0];
				xr[0]=-w*(ar[0]*ar[1]+ai[0]*ai[1])/(p);
				xi[0]=w*(ar[1]*ai[0]-ar[0]*ai[1])/(p);
			}
		}
	
		/**
		 * �ڲ�����
		 */
		private void g65c(ref double x,ref double y,ref double x1,ref double y1,ref double dx,ref double dy,ref double dd,ref double dc,ref double c,ref int k,ref int nis,ref int it)
		{ 
			if (it==0)
			{ 
				nis=1;
				dd=Math.Sqrt(dx*(dx)+dy*(dy));
				if (dd>1.0) 
					dd=1.0;
				dc=6.28/(4.5*(k)); 
				c=0.0;
			}
    
			while(true)
			{ 
				c=c+dc;
				dx=dd*Math.Cos(c); 
				dy=dd*Math.Sin(c);
				x1=x+dx; 
				y1=y+dy;
				if (c<=6.29)
				{ 
					it=0; 
					return;
				}
        
				dd=dd/1.67;
				if (dd<=1.0e-07)
				{ 
					it=1; 
					return;
				}
        
				c=0.0;
			}
		}
	
		/**
		 * ������Է���һ��ʵ�������ؿ��巨
		 * 
		 * ����ʱ���븲�Ǽ��㷽����˺���f(x)ֵ���麯��double Func(double x)
		 * 
		 * @param x - �����ֵ���²�⣩��������õ�ʵ��
		 * @param xStart - ���ȷֲ��Ķ˵��ֵ
		 * @param nControlB - ���Ʋ���
		 * @param eps - ���ƾ���
		 */
		public void GetRootMonteCarlo(ref double x, double xStart, int nControlB, double eps)
		{ 
			int k;
			double xx,a,y,x1,y1,r;
	     
			// �������
			a = xStart; 
			k = 1; 
			r = 1.0;
	
			// ��ֵ
			xx = x; 
			y = Func(xx);
	
			// ���ȿ������
			while (a>=eps)
			{ 
				x1=rnd(ref r);
	
				x1=-a+2.0*a*x1;
				x1=xx+x1; 
				y1=Func(x1);
	         
				k=k+1;
				if (Math.Abs(y1)>=Math.Abs(y))
				{ 
					if (k>nControlB) 
					{ 
						k=1; 
						a=a/2.0; 
					}
				}
				else
				{ 
					k=1; 
					xx=x1; 
					y=y1;
					if (Math.Abs(y)<eps)
					{ 
						x = xx; 
						return; 
					}
				}
			}
	     
			x = xx; 
		}
	
		/**
		 * �ڲ�����
		 */
		private double rnd(ref double r)
		{
			int m;
			double s,u,v,p;
    
			s=65536.0; 
			u=2053.0; 
			v=13849.0;
			m=(int)(r/s); 
			r=r-m*s;
			r=u*r+v; 
			m=(int)(r/s);
			r=r-m*s; p=r/s;
    
			return(p);
		}
	
		/**
		 * ��ʵ�����򸴺�������һ�����������ؿ��巨
		 * 
		 * ����ʱ���븲�Ǽ��㷽����˺�����ģֵ||f(x, y)||���麯��
		 *          double Func(double x, double y)
		 * 
		 * @param x - �����ֵ���²�⣩��ʵ����������õĸ���ʵ��
		 * @param y - �����ֵ���²�⣩���鲿��������õĸ����鲿
		 * @param xStart - ���ȷֲ��Ķ˵��ֵ
		 * @param nControlB - ���Ʋ���
		 * @param eps - ���ƾ���
		 */
		public void GetRootMonteCarlo(ref double x, ref double y, double xStart, int nControlB, double eps)
		{ 
			int k;
			double xx,yy,a,r,z,x1,y1,z1;
	
			// ����������ֵ
			a=xStart; 
			k=1; 
			r=1.0; 
			xx=x; 
			yy=y;
			z=Func(xx,yy);
	     
			// ���ȿ������
			while (a>=eps)
			{ 
				x1=-a+2.0*a*rnd(ref r); 

				x1=xx+x1; 
	 		
				y1=-a+2.0*a*rnd(ref r);
	        
				y1=yy+y1;
	         
				z1=Func(x1,y1);
	         
				k=k+1;
				if (z1>=z)
				{ 
					if (k>nControlB) 
					{ 
						k=1; 
						a=a/2.0; 
					}
				}
				else
				{ 
					k=1; 
					xx=x1; 
					yy=y1;  
					z=z1;
					if (z<eps)
					{ 
						x = xx; 
						y = yy; 
						return; 
					}
				}
			}
	     
			x = xx; 
			y = yy; 
		}
	
		/**
		 * ������Է�����һ��ʵ�����ݶȷ�
		 * 
		 * ����ʱ���븲�Ǽ��㷽����˺���f(x)ֵ����ƫ����ֵ���麯��
		 *          double Func(double x[], double[] y)
		 * 
		 * @param n - ���̵ĸ�����Ҳ��δ֪���ĸ���
		 * @param x - һά���飬����Ϊn�����һ���ֵx0, x1, ��, xn-1��
		 *            ����ʱ��ŷ������һ��ʵ��
		 * @param nMaxIt - ��������
		 * @param eps - ���ƾ���
		 * @return bool �ͣ�����Ƿ�ɹ�
		 */
		public bool GetRootsetGrad(int n, double[] x, int nMaxIt, double eps)
		{ 
			int l,j;
			double f,d,s;
			double[] y = new double[n];
	
			l=nMaxIt;
			f=Func(x,y);
	
			// ���ƾ��ȣ��������
			while (f>=eps)
			{ 
				l=l-1;
				if (l==0) 
				{ 
					return true;
				}
	         
				d=0.0;
				for (j=0; j<=n-1; j++) 
					d=d+y[j]*y[j];
				if (d+1.0==1.0) 
				{ 
					return false;
				}
	         
				s=f/d;
				for (j=0; j<=n-1; j++) 
					x[j]=x[j]-s*y[j];
	         
				f=Func(x,y);
			}
	     
			// �Ƿ�����Ч���������ڴﵽ����
			return (nMaxIt>l);
		}
	
        //
        // ������Է�����һ��ʵ������ţ�ٷ�
        // 
        // ����ʱ���븲�Ǽ��㷽����˺���f(x)ֵ����ƫ����ֵ���麯��
        //          double Func(double[] x, double[] y)
        // 
        // @param n - ���̵ĸ�����Ҳ��δ֪���ĸ���
        // @param x - һά���飬����Ϊn�����һ���ֵx0, x1, ��, xn-1��
        //            ����ʱ��ŷ������һ��ʵ��
        // @param t - ����h��С�ı�����0<t<1
        // @param h - ������ֵ
        // @param nMaxIt - ��������
        // @param eps - ���ƾ���
        // @return bool �ͣ�����Ƿ�ɹ�
        // 
		public bool GetRootsetNewton(int n, double[] x, double t, double h, int nMaxIt, double eps)
		{ 
			int i,j,l;
			double am,z,beta,d;
	
			double[] y = new double[n];
	 	
			// �������
			Matrix mtxCoef = new Matrix(n, n);
			Matrix mtxConst = new Matrix(n, 1);
			double[] a = mtxCoef.GetData();
			double[] b = mtxConst.GetData();
	
			// �������
			l=nMaxIt; 
			am=1.0+eps;
			while (am>=eps)
			{ 
				Func(x,b);
	         
				am=0.0;
				for (i=0; i<=n-1; i++)
				{ 
					z=Math.Abs(b[i]);
					if (z>am) 
						am=z;
				}
	         
				if (am>=eps)
				{ 
					l=l-1;
					if (l==0)
					{ 
						return false;
					}
	             
					for (j=0; j<=n-1; j++)
					{ 
						z=x[j]; 
						x[j]=x[j]+h;
	                 
						Func(x,y);
	                 
						for (i=0; i<=n-1; i++) 
							a[i*n+j]=y[i];
	                 
						x[j]=z;
					}
	             
					// ����ȫѡ��Ԫ��˹��Ԫ��
					LEquations leqs = new LEquations(mtxCoef, mtxConst);
					Matrix mtxResult = new Matrix();
					if (! leqs.GetRootsetGauss(mtxResult))
					{
						return false;
					}
	
					mtxConst.SetValue(mtxResult);
					b = mtxConst.GetData();
	
					beta=1.0;
					for (i=0; i<=n-1; i++) 
						beta=beta-b[i];
	             
					if (beta == 0.0)
					{ 
						return false;
					}
	             
					d=h/beta;
					for (i=0; i<=n-1; i++) 
						x[i]=x[i]-d*b[i];
	             
					h=t*h;
				}
			}
	     
			// �Ƿ�����Ч���������ڴﵽ����
			return (nMaxIt>l);
		}
	
		/**
		 * ������Է�������С���˽�Ĺ����淨
		 * 
		 * ����ʱ��1. �븲�Ǽ��㷽����˺���f(x)ֵ����ƫ����ֵ���麯��
		 *              double Func(double[] x, double[] y)
		 *        2. �븲�Ǽ����ſɱȾ��������麯��
		 *              double FuncMJ(double[] x, double[] y)
		 * 
		 * @param m - ���̵ĸ���
		 * @param n - δ֪���ĸ���
		 * @param x - һά���飬����Ϊn�����һ���ֵx0, x1, ��, xn-1��Ҫ��ȫΪ0��
		 * 			����ʱ��ŷ��������С���˽⣬��m=nʱ�����Ƿ����Է�����Ľ�
		 * @param eps1 - ��С���˽�ľ��ȿ��ƾ���
		 * @param eps2 - ����ֵ�ֽ�ľ��ȿ��ƾ���
		 * @return bool �ͣ�����Ƿ�ɹ�
		 */
		public bool GetRootsetGinv(int m, int n, double[] x, double eps1, double eps2)
		{ 
			int i,j,k,l,kk,jt;
			double alpha,z=0,h2,y1,y2,y3,y0,h1;
			double[] p,d,dx;
	     
			double[] y = new double[10];
			double[] b = new double[10];
	     
			// ���Ʋ���
			int ka = Math.Max(m, n)+1;
			double[] w = new double[ka];
	     
			// �趨��������Ϊ60���������
			l=60; 
			alpha=1.0;
			while (l>0)
			{ 
				Matrix mtxP = new Matrix(m, n);
				Matrix mtxD = new Matrix(m, 1);
				p = mtxP.GetData();
				d = mtxD.GetData();
	
				Func(x,d);
				FuncMJ(x,p);
	
				// �������Է�����
				LEquations leqs = new LEquations(mtxP, mtxD);
				// ��ʱ����
				Matrix mtxAP = new Matrix();
				Matrix mtxU = new Matrix();
				Matrix mtxV = new Matrix();
				// �����
				Matrix mtxDX = new Matrix();
				// ���ڹ��������С���˽�
				if (! leqs.GetRootsetGinv(mtxDX, mtxAP, mtxU, mtxV, eps2))
				{ 
					return false;
				}
	         
				dx = mtxDX.GetData();
	
				j=0; 
				jt=1; 
				h2=0.0;
				while (jt==1)
				{ 
					jt=0;
					if (j<=2) 
						z=alpha+0.01*j;
					else 
						z=h2;
	             
					for (i=0; i<=n-1; i++) 
						w[i]=x[i]-z*dx[i];
	             
					Func(w,d);
	             
					y1=0.0;
					for (i=0; i<=m-1; i++) 
						y1=y1+d[i]*d[i];
					for (i=0; i<=n-1; i++)
						w[i]=x[i]-(z+0.00001)*dx[i];
	             
					Func(w,d);
	             
					y2=0.0;
					for (i=0; i<=m-1; i++) 
						y2=y2+d[i]*d[i];
	             
					y0=(y2-y1)/0.00001;
	             
					if (Math.Abs(y0)>1.0e-10)
					{ 
						h1=y0; h2=z;
						if (j==0) 
						{ 
							y[0]=h1; 
							b[0]=h2;
						}
						else
						{ 
							y[j]=h1; 
							kk=0; 
							k=0;
							while ((kk==0)&&(k<=j-1))
							{ 
								y3=h2-b[k];
								if (Math.Abs(y3)+1.0==1.0) 
									kk=1;
								else 
									h2=(h1-y[k])/y3;
	                         
								k=k+1;
							}
	                     
							b[j]=h2;
							if (kk!=0) 
								b[j]=1.0e+35;
	                     
							h2=0.0;
							for (k=j-1; k>=0; k--)
								h2=-y[k]/(b[k+1]+h2);
	                     
							h2=h2+b[0];
						}
	                 
						j=j+1;
						if (j<=7) 
							jt=1;
						else 
							z=h2;
					}
				}
	         
				alpha=z; 
				y1=0.0; 
				y2=0.0;
				for (i=0; i<=n-1; i++)
				{ 
					dx[i]=-alpha*dx[i];
					x[i]=x[i]+dx[i];
					y1=y1+Math.Abs(dx[i]);
					y2=y2+Math.Abs(x[i]);
				}
	         
				// ���ɹ�
				if (y1<eps1*y2)
				{ 
					return true;
				}
	         
				l=l-1;
			}
	     
			// ���ʧ��
			return false;
		}
	
		/**
		 * ������Է�����һ��ʵ�������ؿ��巨
		 * 
		 * ����ʱ���븲�Ǽ��㷽�����ģ����ֵ||F||���麯��
		 *          double Func(int n, double[] x)
		 * �䷵��ֵΪSqr(f1*f1 + f2*f2 + �� + fn*fn)
		 * 
		 * @param n - ���̵ĸ�����Ҳ��δ֪���ĸ���
		 * @param x - һά���飬����Ϊn�����һ���ֵx0, x1, ��, xn-1��
		 *            ����ʱ��ŷ������һ��ʵ��
		 * @param xStart - ���ȷֲ��Ķ˵��ֵ
		 * @param nControlB - ���Ʋ���
		 * @param eps - ���ƾ���
		 */
		public void GetRootsetMonteCarlo(int n, double[] x, double xStart, int nControlB, double eps)
		{ 
			int k,i;
			double a,r,z,z1;
	
			double[] y = new double[n];
	     
			// ��ֵ
			a=xStart; 
			k=1; 
			r=1.0; 
	
			z = Func(x);
	
			// �þ��ȿ��Ƶ������
			while (a>=eps)
			{ 
				for (i=0; i<=n-1; i++)
				{
					y[i]=-a+2.0*a*rnd(ref r)+x[i];
				}
				z1=Func(y);
	         
				k=k+1;
				if (z1>=z)
				{ 
					if (k>nControlB) 
					{ 
						k=1; 
						a=a/2.0; 
					}
				}
				else
				{ 
					k=1; 
					for (i=0; i<=n-1; i++) 
						x[i]=y[i];
	             
					// ���ɹ�
					z=z1;
					if (z<eps)  
					{
						return;
					}
				}
			}
		}
	}
}