using System;
using System.Text;

namespace basicengine
{
	public class Matrix
	{
		public Matrix()
		{
		}

		public int M
		{
			get
			{
				if (Data == null)
				{
					return 0;
				}

				return Data.GetLength(0);
			}
		}

		public int N
		{
			get
			{
				if (Data == null)
				{
					return 0;
				}

				return Data.GetLength(1);
			}
		}

		public double[,] Data { get; set; }

		Matrix transpose;
		public Matrix Transpose {
			get
			{
				if (Data == null)
				{
					return null;
				}

				if (transpose == null)
				{
					transpose = new Matrix();
					transpose.Data = new double[N, M];
				}

				for (int i = 0; i < N; i++)
				{
					for (int j = 0; j < M; j++)
					{
						transpose.Data[i, j] = Data[j, i];
					}
				}


				return transpose;
			}
		}

		Matrix temp;
		public Matrix DOT(Matrix b)
		{
			if (this.N != b.M)
			{
				throw new Exception(string.Format("Number of columns in First Matrix ({0}) should be equal to Number of rows in Second Matrix ({1}).",this.N,b.M));
			}

			if (temp == null)
			{
				temp = new Matrix();
			}

			temp.Data = new double[this.M, b.N];
			for (int i = 0; i < temp.M; i++)
			{
				for (int j = 0; j < temp.N; j++)
				{
					temp.Data[i, j] = 0;
					for (int k = 0; k < this.N; k++) // OR k<b.GetLength(0)
						temp.Data[i, j] = temp.Data[i, j] + Data[i, k] * b.Data[k, j];
				}
			}


			return temp;
		}

		public Matrix CROSS(Matrix b)
		{
			if ((this.N != 1) && (this.M != 1))
			{
				throw new Exception(string.Format("Vector Cross Operatiion is not Allowed.(M:{0} N:{1})", this.M, this.N));
			}

			if (temp == null)
			{
				temp = new Matrix();
			}

			if (this.M > 1)
			{
				temp.Data = new double[b.M, b.N];
				for (int i = 0; i < b.M; i++)
				{
					for (int j = 0; j < b.N; j++)
					{
						temp.Data[i, j] = this.Data[i, 0] * b.Data[i, j];
					}
				}

			}
			else {
				temp.Data = new double[1,N];
				if (this.N == b.N)
				{
					for (int i = 0; i < b.M; i++)
					{
						for (int j = 0; j < b.N; j++)
						{
							temp.Data[i, j] = this.Data[0, j] * b.Data[i, j];
						}

					}
				}
			}

			return temp;
		}


		public Matrix ScallerProduct(double x)
		{
			if (temp == null)
			{
				temp = new Matrix();
			}

			temp.Data = new double[M,N];
			for (int i = 0; i < temp.M; i++)
			{
				for (int j = 0; j < temp.N; j++)
				{
					temp.Data[i, j] = x * this.Data[i,j];
				}
			}


			return temp;
		}

		public Matrix Subtract(Matrix b)
		{
			if (this.M != b.M)
			{
				throw new Exception(string.Format("Number of columns in First Matrix ({0}) should be equal to Number of columns in Second Matrix ({1}).",this.M,b.M));
			}
			if (this.N != b.N)
			{
				throw new Exception(string.Format("Number of rows in First Matrix ({0}) should be equal to Number of rows in Second Matrix ({1}).",this.N,b.N));
			}

			if (temp == null)
			{
				temp = new Matrix();
			}

			temp.Data = new double[this.M, this.N];
			for (int i = 0; i < temp.M; i++)
			{
				for (int j = 0; j < temp.N; j++)
				{
					temp.Data[i, j] = this.Data[i, j] - b.Data[i, j];
				}
			}

			return temp;
		}

		public Matrix Add(Matrix b)
		{
			if (this.M != b.M)
			{
				throw new Exception(string.Format("Number of columns in First Matrix ({0}) should be equal to Number of columns in Second Matrix ({1}).", this.M, b.M));
			}
			if (this.N != b.N)
			{
				throw new Exception(string.Format("Number of rows in First Matrix ({0}) should be equal to Number of rows in Second Matrix ({1}).", this.N, b.N));
			}

			if (temp == null)
			{
				temp = new Matrix();
			}

			temp.Data = new double[this.M, this.N];
			for (int i = 0; i < temp.M; i++)
			{
				for (int j = 0; j < temp.N; j++)
				{
					temp.Data[i, j] = this.Data[i, j] + b.Data[i, j];
				}
			}

			return temp;
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("(M:{0} N:{1}){2}",M,N,Environment.NewLine);
			for (int i = 0; i < M; i++)
			{
				for (int j = 0; j < N; j++)
				{
					sb.AppendFormat("{0} ", this.Data[i, j]);
				}
				sb.Append(Environment.NewLine);
			}
			sb.Append(Environment.NewLine);

			return sb.ToString();

			//return string.Format("[Matrix: M={0}, N={1}, Data={2}, Transpose={3}]", M, N, Data, Transpose);
		}
	}
}
