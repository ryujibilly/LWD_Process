/*
 * 操作复数的类Complex
 *
 */

using System;

namespace LWD_DataProcess
{
	/**
	 * 操作复数的类Complex
	 * @version 1.0
	 */
	public class Complex 
	{
		private double real = 0.0;			// 复数的实部
		private double imaginary = 0.0;		// 复数的虚部
		private double eps = 0.0;           // 缺省精度

		/**
		 * 属性: 实部
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
		 * 属性: 虚部
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
		 * 属性: Eps
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
		 * 基本构造函数
		 */
		public Complex() 
		{
		}

		/**
		 * 指定值构造函数
		 * 
		 * @param dblX - 指定的实部
		 * @param dblY - 指定的虚部
		 */
		public Complex(double dblX, double dblY)
		{
			real = dblX;
			imaginary = dblY;
		}

		/**
		 * 拷贝构造函数
		 * 
		 * @param other - 源复数
		 */
		public Complex(Complex other)
		{
			real = other.real;
			imaginary = other.imaginary;
		}

		/**
		 * 根据"a,b"形式的字符串来构造复数，以a为复数的实部，b为复数的虚部
		 * 
		 * @param s - "a,b"形式的字符串，a为复数的实部，b为复数的虚部
		 * @param sDelim - a, b之间的分隔符
		 */
		public Complex(string s, string sDelim)
		{
			SetValue(s, sDelim);
		}

		/**
		 * 设置复数运算的精度
		 * 
		 * @param newEps - 新的精度值
		 */
		public void SetEps(double newEps)
		{
			eps = newEps;
		}
	
		/**
		 * 取复数的精度值
		 * 
		 * @return double型，复数的精度值
		 */
		public double GetEps()
		{
			return eps;
		}

		/**
		 * 指定复数的实部
		 * 
		 * @param dblX - 复数的实部
		 */
		public void SetReal(double dblX)
		{
			real = dblX;
		}

		/**
		 * 指定复数的虚部
		 * 
		 * @param dblY - 复数的虚部
		 */
		public void SetImag(double dblY)
		{
			imaginary = dblY;
		}

		/**
		 * 取复数的实部
		 * 
		 * @return double 型，复数的实部
		 */
		public double GetReal()
		{
			return real;
		}

		/**
		 * 取复数的虚部
		 * 
		 * @return double 型，复数的虚部
		 */
		public double GetImag()
		{
			return imaginary;
		}

		/**
		 * 指定复数的实部和虚部值
		 * 
		 * @param real - 指定的实部
		 * @param imag - 指定的虚部
		 */
		public void SetValue(double real, double imag)
		{
			SetReal(real);
			SetImag(imag);
		}
	
		/**
		 * 将"a,b"形式的字符串转化为复数，以a为复数的实部，b为复数的虚部
		 * 
		 * @param s - "a,b"形式的字符串，a为复数的实部，b为复数的虚部
		 * @param sDelim - a, b之间的分隔符
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
		 * 重载 + 运算符
		 * 
		 * @return Complex对象
		 */
		public static Complex operator +(Complex cpx1, Complex cpx2)
		{
			return cpx1.Add(cpx2);
		}

		/**
		 * 重载 - 运算符
		 * 
		 * @return Complex对象
		 */
		public static Complex operator -(Complex cpx1, Complex cpx2)
		{
			return cpx1.Subtract(cpx2);
		}
		
		/**
		 * 重载 * 运算符
		 * 
		 * @return Complex对象
		 */
		public static Complex operator *(Complex cpx1, Complex cpx2)
		{
			return cpx1.Multiply(cpx2);
		}

		/**
		 * 重载 / 运算符
		 * 
		 * @return Complex对象
		 */
		public static Complex operator /(Complex cpx1, Complex cpx2)
		{
			return cpx1.Divide(cpx2);
		}
		
		/**
		 * 重载 double 运算符
		 * 
		 * @return double值
		 */
		public static implicit operator double(Complex cpx)
		{
			return cpx.Abs();
		}

		/**
		 * 将复数转化为"a+bj"形式的字符串
		 * 
		 * @return string 型，"a+bj"形式的字符串
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
		 * 比较两个复数是否相等
		 * 
		 * @param other - 用于比较的复数
		 * @return bool型，相等则为true，否则为false
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
		 * 因为重写了Equals，因此必须重写GetHashCode
		 * 
		 * @return int型，返回复数对象散列码
		 */
		public override int GetHashCode()
		{
			return (int)Math.Sqrt(real * real + imaginary * imaginary);
		}

		/**
		 * 给复数赋值
		 * 
		 * @param cpxX - 用于给复数赋值的源复数
		 * @return Complex型，与cpxX相等的复数
		 */
		public Complex SetValue(Complex cpxX)
		{
			real = cpxX.real;
			imaginary = cpxX.imaginary;

			return this;
		}

		/**
		 * 实现复数的加法
		 * 
		 * @param cpxX - 与指定复数相加的复数
		 * @return Complex型，指定复数与cpxX相加之和
		 */
		public Complex Add(Complex cpxX)
		{
			double x = real + cpxX.real;
			double y = imaginary + cpxX.imaginary;

			return new Complex(x, y);
		}

		/**
		 * 实现复数的减法
		 * 
		 * @param cpxX - 与指定复数相减的复数
		 * @return Complex型，指定复数减去cpxX之差
		 */
		public Complex Subtract(Complex cpxX)
		{
			double x = real - cpxX.real;
			double y = imaginary - cpxX.imaginary;

			return new Complex(x, y);
		}

		/**
		 * 实现复数的乘法
		 * 
		 * @param cpxX - 与指定复数相乘的复数
		 * @return Complex型，指定复数与cpxX相乘之积
		 */
		public Complex Multiply(Complex cpxX)
		{
			double x = real * cpxX.real - imaginary * cpxX.imaginary;
			double y = real * cpxX.imaginary + imaginary * cpxX.real;

			return new Complex(x, y);
		}

		/**
		 * 实现复数的除法
		 * 
		 * @param cpxX - 与指定复数相除的复数
		 * @return Complex型，指定复数除与cpxX之商
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
		 * 计算复数的模
		 * 
		 * @return double型，指定复数的模
		 */
		public double Abs()
		{
			// 求取实部和虚部的绝对值
			double x = Math.Abs(real);
			double y = Math.Abs(imaginary);

			if (real == 0)
				return y;
			if (imaginary == 0)
				return x;
	    
	    
			// 计算模
			if (x > y)
				return (x * Math.Sqrt(1 + (y / x) * (y / x)));
	    
			return (y * Math.Sqrt(1 + (x / y) * (x / y)));
		}

		/**
		 * 计算复数的根
		 * 
		 * @param n - 待求根的根次
		 * @param cpxR - Complex型数组，长度为n，返回复数的所有根
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
		 * 计算复数的实幂指数
		 * 
		 * @param dblW - 待求实幂指数的幂次
		 * @return Complex型，复数的实幂指数值
		 */
		public Complex Pow(double dblW)
		{
			// 常量
			const double PI = 3.14159265358979;

			// 局部变量
			double r, t;
	    
			// 特殊值处理
			if ((real == 0) && (imaginary == 0))
				return new Complex(0, 0);
	    
			// 幂运算公式中的三角函数运算
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
	    
			// 模的幂
			r = Math.Exp(dblW * Math.Log(Math.Sqrt(real * real + imaginary * imaginary)));
	    
			// 复数的实幂指数
			return new Complex(r * Math.Cos(dblW * t), r * Math.Sin(dblW * t));
		}

		/**
		 * 计算复数的复幂指数
		 * 
		 * @param cpxW - 待求复幂指数的幂次
		 * @param n - 控制参数，默认值为0。当n=0时，求得的结果为复幂指数的主值
		 * @return Complex型，复数的复幂指数值
		 */
		public Complex Pow(Complex cpxW, int n)
		{
			// 常量
			const double PI = 3.14159265358979;
			// 局部变量
			double r, s, u, v;
	    
			// 特殊值处理
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
	    
			// 求幂运算公式
			r = 0.5 * Math.Log(real * real + imaginary * imaginary);
			v = cpxW.real * r + cpxW.imaginary * s;
			u = Math.Exp(cpxW.real * r - cpxW.imaginary * s);

			return new Complex(u * Math.Cos(v), u * Math.Sin(v));
		}

		/**
		 * 计算复数的自然对数
		 * 
		 * @return Complex型，复数的自然对数值
		 */
		public Complex Log()
		{
			double p = Math.Log(Math.Sqrt(real*real + imaginary*imaginary));
			return new Complex(p, Math.Atan2(imaginary, real));
		}

		/**
		 * 计算复数的正弦
		 * 
		 * @return Complex型，复数的正弦值
		 */
		public Complex Sin()
		{
			int i;
			double x, y, y1, br, b1, b2;
			double[] c = new double[6];
	    
			// 切比雪夫公式的常数系数
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
	    
			// 组合计算结果
			x = x * Math.Sin(real);
			y = y * Math.Cos(real);

			return new Complex(x, y);
		}

		/**
		 * 计算复数的余弦
		 * 
		 * @return Complex型，复数的余弦值
		 */
		public Complex Cos()
		{
			int i;
			double x, y, y1, br, b1, b2;
			double[] c = new double[6];
	    
			// 切比雪夫公式的常数系数
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
	    
			// 组合计算结果
			x = x * Math.Cos(real);
			y = -y * Math.Sin(real);

			return new Complex(x, y);
		}

		/**
		 * 计算复数的正切
		 * 
		 * @return Complex型，复数的正切值
		 */
		public Complex Tan()
		{
			return Sin().Divide(Cos());
		}
	}
}