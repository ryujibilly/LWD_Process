/*
 * ������������Complex
 *
 */

using System;

namespace LWD_DataProcess
{
	/**
	 * ������������Complex
	 * @version 1.0
	 */
	public class Complex 
	{
		private double real = 0.0;			// ������ʵ��
		private double imaginary = 0.0;		// �������鲿
		private double eps = 0.0;           // ȱʡ����

		/**
		 * ����: ʵ��
		 */
		public double Real
		{
			get
			{
				return real;
			}
			set
			{
				real = value;
			}
		}

		/**
		 * ����: �鲿
		 */
		public double Imaginary
		{
			get
			{
				return imaginary;
			}
			set
			{
				imaginary = value;
			}
		}

		/**
		 * ����: Eps
		 */
		public double Eps
		{
			get
			{
				return eps;
			}
			set
			{
				eps = value;
			}
		}

		/**
		 * �������캯��
		 */
		public Complex() 
		{
		}

		/**
		 * ָ��ֵ���캯��
		 * 
		 * @param dblX - ָ����ʵ��
		 * @param dblY - ָ�����鲿
		 */
		public Complex(double dblX, double dblY)
		{
			real = dblX;
			imaginary = dblY;
		}

		/**
		 * �������캯��
		 * 
		 * @param other - Դ����
		 */
		public Complex(Complex other)
		{
			real = other.real;
			imaginary = other.imaginary;
		}

		/**
		 * ����"a,b"��ʽ���ַ��������츴������aΪ������ʵ����bΪ�������鲿
		 * 
		 * @param s - "a,b"��ʽ���ַ�����aΪ������ʵ����bΪ�������鲿
		 * @param sDelim - a, b֮��ķָ���
		 */
		public Complex(string s, string sDelim)
		{
			SetValue(s, sDelim);
		}

		/**
		 * ���ø�������ľ���
		 * 
		 * @param newEps - �µľ���ֵ
		 */
		public void SetEps(double newEps)
		{
			eps = newEps;
		}
	
		/**
		 * ȡ�����ľ���ֵ
		 * 
		 * @return double�ͣ������ľ���ֵ
		 */
		public double GetEps()
		{
			return eps;
		}

		/**
		 * ָ��������ʵ��
		 * 
		 * @param dblX - ������ʵ��
		 */
		public void SetReal(double dblX)
		{
			real = dblX;
		}

		/**
		 * ָ���������鲿
		 * 
		 * @param dblY - �������鲿
		 */
		public void SetImag(double dblY)
		{
			imaginary = dblY;
		}

		/**
		 * ȡ������ʵ��
		 * 
		 * @return double �ͣ�������ʵ��
		 */
		public double GetReal()
		{
			return real;
		}

		/**
		 * ȡ�������鲿
		 * 
		 * @return double �ͣ��������鲿
		 */
		public double GetImag()
		{
			return imaginary;
		}

		/**
		 * ָ��������ʵ�����鲿ֵ
		 * 
		 * @param real - ָ����ʵ��
		 * @param imag - ָ�����鲿
		 */
		public void SetValue(double real, double imag)
		{
			SetReal(real);
			SetImag(imag);
		}
	
		/**
		 * ��"a,b"��ʽ���ַ���ת��Ϊ��������aΪ������ʵ����bΪ�������鲿
		 * 
		 * @param s - "a,b"��ʽ���ַ�����aΪ������ʵ����bΪ�������鲿
		 * @param sDelim - a, b֮��ķָ���
		 */
		public void SetValue(string s, string sDelim)
		{
			int nPos = s.IndexOf(sDelim);
			if (nPos == -1)
			{
				s = s.Trim();
				real = Double.Parse(s);
				imaginary = 0;
			}
			else
			{
				int nLen = s.Length;
				string sLeft = s.Substring(0, nPos);
				string sRight = s.Substring(nPos+1, nLen-nPos-1);
				sLeft = sLeft.Trim();
				sRight = sRight.Trim();
				real = Double.Parse(sLeft);
				imaginary = Double.Parse(sRight);
			}
		}

		/**
		 * ���� + �����
		 * 
		 * @return Complex����
		 */
		public static Complex operator +(Complex cpx1, Complex cpx2)
		{
			return cpx1.Add(cpx2);
		}

		/**
		 * ���� - �����
		 * 
		 * @return Complex����
		 */
		public static Complex operator -(Complex cpx1, Complex cpx2)
		{
			return cpx1.Subtract(cpx2);
		}
		
		/**
		 * ���� * �����
		 * 
		 * @return Complex����
		 */
		public static Complex operator *(Complex cpx1, Complex cpx2)
		{
			return cpx1.Multiply(cpx2);
		}

		/**
		 * ���� / �����
		 * 
		 * @return Complex����
		 */
		public static Complex operator /(Complex cpx1, Complex cpx2)
		{
			return cpx1.Divide(cpx2);
		}
		
		/**
		 * ���� double �����
		 * 
		 * @return doubleֵ
		 */
		public static implicit operator double(Complex cpx)
		{
			return cpx.Abs();
		}

		/**
		 * ������ת��Ϊ"a+bj"��ʽ���ַ���
		 * 
		 * @return string �ͣ�"a+bj"��ʽ���ַ���
		 */
		public override string ToString()
		{
			string s;
			if (real != 0.0)
			{
				if (imaginary > 0)
					s = real.ToString("F") + "+" + imaginary.ToString("F") + "j";
				else if (imaginary < 0)
				{
					double absImag = -1*imaginary;
					s = real.ToString("F") + "-" + absImag.ToString("F") + "j";
				}
				else
					s = real.ToString("F");
			}
			else
			{
				if (imaginary > 0)
					s = imaginary.ToString("F") + "j";
				else if (imaginary < 0)
				{
					double absImag = -1*imaginary;
					s = absImag.ToString("F") + "j";
				}
				else
					s = real.ToString("F");
			}

			return s;
		}

		/**
		 * �Ƚ����������Ƿ����
		 * 
		 * @param other - ���ڱȽϵĸ���
		 * @return bool�ͣ������Ϊtrue������Ϊfalse
		 */
		public override bool Equals(object other)
		{
			Complex cpxX = other as Complex;
			if (cpxX == null)
				return false;
			return Math.Abs(real - cpxX.real) <= eps && 
				Math.Abs(imaginary - cpxX.imaginary) <= eps; 
		}

		/**
		 * ��Ϊ��д��Equals����˱�����дGetHashCode
		 * 
		 * @return int�ͣ����ظ�������ɢ����
		 */
		public override int GetHashCode()
		{
			return (int)Math.Sqrt(real * real + imaginary * imaginary);
		}

		/**
		 * ��������ֵ
		 * 
		 * @param cpxX - ���ڸ�������ֵ��Դ����
		 * @return Complex�ͣ���cpxX��ȵĸ���
		 */
		public Complex SetValue(Complex cpxX)
		{
			real = cpxX.real;
			imaginary = cpxX.imaginary;

			return this;
		}

		/**
		 * ʵ�ָ����ļӷ�
		 * 
		 * @param cpxX - ��ָ��������ӵĸ���
		 * @return Complex�ͣ�ָ��������cpxX���֮��
		 */
		public Complex Add(Complex cpxX)
		{
			double x = real + cpxX.real;
			double y = imaginary + cpxX.imaginary;

			return new Complex(x, y);
		}

		/**
		 * ʵ�ָ����ļ���
		 * 
		 * @param cpxX - ��ָ����������ĸ���
		 * @return Complex�ͣ�ָ��������ȥcpxX֮��
		 */
		public Complex Subtract(Complex cpxX)
		{
			double x = real - cpxX.real;
			double y = imaginary - cpxX.imaginary;

			return new Complex(x, y);
		}

		/**
		 * ʵ�ָ����ĳ˷�
		 * 
		 * @param cpxX - ��ָ��������˵ĸ���
		 * @return Complex�ͣ�ָ��������cpxX���֮��
		 */
		public Complex Multiply(Complex cpxX)
		{
			double x = real * cpxX.real - imaginary * cpxX.imaginary;
			double y = real * cpxX.imaginary + imaginary * cpxX.real;

			return new Complex(x, y);
		}

		/**
		 * ʵ�ָ����ĳ���
		 * 
		 * @param cpxX - ��ָ����������ĸ���
		 * @return Complex�ͣ�ָ����������cpxX֮��
		 */
		public Complex Divide(Complex cpxX)
		{
			double e, f, x, y;
	    
			if (Math.Abs(cpxX.real) >= Math.Abs(cpxX.imaginary))
			{
				e = cpxX.imaginary / cpxX.real;
				f = cpxX.real + e * cpxX.imaginary;
	        
				x = (real + imaginary * e) / f;
				y = (imaginary - real * e) / f;
			}
			else
			{
				e = cpxX.real / cpxX.imaginary;
				f = cpxX.imaginary + e * cpxX.real;
	        
				x = (real * e + imaginary) / f;
				y = (imaginary * e - real) / f;
			}

			return new Complex(x, y);
		}

		/**
		 * ���㸴����ģ
		 * 
		 * @return double�ͣ�ָ��������ģ
		 */
		public double Abs()
		{
			// ��ȡʵ�����鲿�ľ���ֵ
			double x = Math.Abs(real);
			double y = Math.Abs(imaginary);

			if (real == 0)
				return y;
			if (imaginary == 0)
				return x;
	    
	    
			// ����ģ
			if (x > y)
				return (x * Math.Sqrt(1 + (y / x) * (y / x)));
	    
			return (y * Math.Sqrt(1 + (x / y) * (x / y)));
		}

		/**
		 * ���㸴���ĸ�
		 * 
		 * @param n - ������ĸ���
		 * @param cpxR - Complex�����飬����Ϊn�����ظ��������и�
		 */
		public void Root(int n, Complex[] cpxR)
		{
			if (n<1) 
				return;
	    
			double q = Math.Atan2(imaginary, real);
			double r = Math.Sqrt(real*real + imaginary*imaginary);
			if (r != 0)
			{ 
				r = (1.0/n)*Math.Log(r);
				r = Math.Exp(r);
			}

			for (int k=0; k<=n-1; k++)
			{ 
				double t = (2.0*k*3.1415926+q)/n;
				cpxR[k] = new Complex(r*Math.Cos(t), r*Math.Sin(t));
			}
		}

		/**
		 * ���㸴����ʵ��ָ��
		 * 
		 * @param dblW - ����ʵ��ָ�����ݴ�
		 * @return Complex�ͣ�������ʵ��ָ��ֵ
		 */
		public Complex Pow(double dblW)
		{
			// ����
			const double PI = 3.14159265358979;

			// �ֲ�����
			double r, t;
	    
			// ����ֵ����
			if ((real == 0) && (imaginary == 0))
				return new Complex(0, 0);
	    
			// �����㹫ʽ�е����Ǻ�������
			if (real == 0)
			{
				if (imaginary > 0)
					t = 1.5707963268;
				else
					t = -1.5707963268;
			}
			else
			{
				if (real > 0)
					t = Math.Atan2(imaginary, real);
				else
				{
					if (imaginary >= 0)
						t = Math.Atan2(imaginary, real) + PI;
					else
						t = Math.Atan2(imaginary, real) - PI;
				}
			}
	    
			// ģ����
			r = Math.Exp(dblW * Math.Log(Math.Sqrt(real * real + imaginary * imaginary)));
	    
			// ������ʵ��ָ��
			return new Complex(r * Math.Cos(dblW * t), r * Math.Sin(dblW * t));
		}

		/**
		 * ���㸴���ĸ���ָ��
		 * 
		 * @param cpxW - ������ָ�����ݴ�
		 * @param n - ���Ʋ�����Ĭ��ֵΪ0����n=0ʱ����õĽ��Ϊ����ָ������ֵ
		 * @return Complex�ͣ������ĸ���ָ��ֵ
		 */
		public Complex Pow(Complex cpxW, int n)
		{
			// ����
			const double PI = 3.14159265358979;
			// �ֲ�����
			double r, s, u, v;
	    
			// ����ֵ����
			if (real == 0)
			{
				if (imaginary == 0)
					return new Complex(0, 0);
	            
				s = 1.5707963268 * (Math.Abs(imaginary) / imaginary + 4 * n);
			}
			else
			{
				s = 2 * PI * n + Math.Atan2(imaginary, real);
	        
				if (real < 0)
				{
					if (imaginary > 0)
						s = s + PI;
					else
						s = s - PI;
				}
			}
	    
			// �������㹫ʽ
			r = 0.5 * Math.Log(real * real + imaginary * imaginary);
			v = cpxW.real * r + cpxW.imaginary * s;
			u = Math.Exp(cpxW.real * r - cpxW.imaginary * s);

			return new Complex(u * Math.Cos(v), u * Math.Sin(v));
		}

		/**
		 * ���㸴������Ȼ����
		 * 
		 * @return Complex�ͣ���������Ȼ����ֵ
		 */
		public Complex Log()
		{
			double p = Math.Log(Math.Sqrt(real*real + imaginary*imaginary));
			return new Complex(p, Math.Atan2(imaginary, real));
		}

		/**
		 * ���㸴��������
		 * 
		 * @return Complex�ͣ�����������ֵ
		 */
		public Complex Sin()
		{
			int i;
			double x, y, y1, br, b1, b2;
			double[] c = new double[6];
	    
			// �б�ѩ��ʽ�ĳ���ϵ��
			c[0] = 1.13031820798497;
			c[1] = 0.04433684984866;
			c[2] = 0.00054292631191;
			c[3] = 0.00000319843646;
			c[4] = 0.00000001103607;
			c[5] = 0.00000000002498;
	    
			y1 = Math.Exp(imaginary);
			x = 0.5 * (y1 + 1 / y1);
			br = 0;
			if (Math.Abs(imaginary) >= 1)
				y = 0.5 * (y1 - 1 / y1);
			else
			{
				b1 = 0;
				b2 = 0;
				y1 = 2 * (2 * imaginary * imaginary - 1);
				for (i = 5; i >=0; --i)
				{
					br = y1 * b1 - b2 - c[i];
					if (i != 0)
					{
						b2 = b1;
						b1 = br;
					}
				}
	        
				y = imaginary * (br - b1);
			}
	    
			// ��ϼ�����
			x = x * Math.Sin(real);
			y = y * Math.Cos(real);

			return new Complex(x, y);
		}

		/**
		 * ���㸴��������
		 * 
		 * @return Complex�ͣ�����������ֵ
		 */
		public Complex Cos()
		{
			int i;
			double x, y, y1, br, b1, b2;
			double[] c = new double[6];
	    
			// �б�ѩ��ʽ�ĳ���ϵ��
			c[0] = 1.13031820798497;
			c[1] = 0.04433684984866;
			c[2] = 0.00054292631191;
			c[3] = 0.00000319843646;
			c[4] = 0.00000001103607;
			c[5] = 0.00000000002498;
	    
			y1 = Math.Exp(imaginary);
			x = 0.5 * (y1 + 1 / y1);
			br = 0;
			if (Math.Abs(imaginary) >= 1)
				y = 0.5 * (y1 - 1 / y1);
			else
			{
				b1 = 0;
				b2 = 0;
				y1 = 2 * (2 * imaginary * imaginary - 1);
				for (i=5 ; i>=0; --i)
				{
					br = y1 * b1 - b2 - c[i];
					if (i != 0)
					{
						b2 = b1;
						b1 = br;
					}
				}
	        
				y = imaginary * (br - b1);
			}
	    
			// ��ϼ�����
			x = x * Math.Cos(real);
			y = -y * Math.Sin(real);

			return new Complex(x, y);
		}

		/**
		 * ���㸴��������
		 * 
		 * @return Complex�ͣ�����������ֵ
		 */
		public Complex Tan()
		{
			return Sin().Divide(Cos());
		}
	}
}