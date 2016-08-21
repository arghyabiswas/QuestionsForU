using System;
namespace basicengine
{
	public class neuralnetwork
	{
		Random rn;
		Matrix x;
		Matrix y;

		Matrix syn0;
		Matrix syn1;

		public neuralnetwork()
		{
			
			rn = new Random();

			x = new Matrix();
			y = new Matrix();

			syn0 = new Matrix();
			syn1 = new Matrix();
		

			x.Data = new double[4, 3]
			{{0, 0, 1 },
			{0, 1, 1},
			{1, 0, 1},
			{1, 1, 1} };

			y.Data = new double[1, 4] { { 0, 1, 1, 0 } };

			syn0.Data = new double[3, 4] {
				{Random(1,4),Random(1,4),Random(1,4),Random(1,4)},
				{Random(1,4),Random(1,4),Random(1,4),Random(1,4)},
				{Random(1,4),Random(1,4),Random(1,4),Random(1,4)},
				};
			syn1.Data = new double[4, 1] {
				{Random(1,4)},
				{Random(1,4)},
				{Random(1,4)},
				{Random(1,4)}
			};

			Train();
		}

		double Sigmoid(double z, bool deriv)
		{
			if (deriv)
			{
				z = z * (1.0 - z);
				return z;
			}

			z = 1.0 / (1.0 + Math.Exp(-1.0 * z));
			return z;
		}

		private Matrix Sigmoid(Matrix c, bool deriv)
		{
			var z = new Matrix();
			z.Data = new double[c.M, c.N];

			for (int i = 0; i < c.M; i++)
			{
				for (int j = 0; j < c.N; j++)
				{
					z.Data[i, j] = Sigmoid(c.Data[i, j], deriv);
				}
			}

			return z;
		}


		int Random(int m,int n)
		{
			return (2 * rn.Next(m, n) - 1);
		}

		// training
		void Train()
		{
			Print("Matrix X:",x.ToString());
			Print("Matrix Y:",y.ToString());
			//Print("Matrix Syn0:",syn0.ToString());
			//Print("Matrix Syn1:",syn1.ToString());
			for (int i = 0; i < 60000; i++)
			{
				var l0 = x;
				//Print("l0 dot syn0 :",l0.DOT(syn0).ToString());
				var l1 = Sigmoid(l0.DOT(syn0),false);						// l1 = S(l0 . s0)
				//Print("Matrix l1:",l1.ToString());							

				//Print("l1 dot syn1 :", l1.DOT(syn1).ToString());
				var l2 = Sigmoid(l1.DOT(syn1),false);						// l2 = S(l1 . s1)
				//Print("Matrix l2:", l2.ToString());

				var l2_error = y.Subtract( l2.Transpose);					// e2 = y - l2 // vector
				//Print("Matrix l2_error:", l2_error.ToString());

				var l2_delta = Sigmoid(l2, true).CROSS(l2_error);// l2.Transpose.CROSS(Sigmoid(l2_error,true));	// l2T . S-1(e2) // e2 * S-1(l2)
				//Print("Matrix l2_delta:", l2_delta.ToString());
				//break;
				var l1_error = l2_delta.Transpose.DOT( syn1.Transpose);
				//Print("Matrix l1_error:", l1_error.ToString());
				//Print("Matrix l1_error:", l1_error.ToString());
				var l1_delta = l1_error.DOT(Sigmoid(l1, true)); // should be cross
																		 //Print("Matrix l1_delta:", l1_delta.ToString());
				//break;
				// Update Wait
				// Gradient Descent
				syn1 = syn1.Add(l1.DOT(l2_delta.Transpose)); // * syn1;
				//Print("syn1", syn1.ToString());

				syn0 = syn0.Add(l0.Transpose.DOT(l1_delta)); // * syn0
				//Print("syn0", syn0.ToString());

				if (i % 10000 == 0)
				{
					Print("l2_error",l2_error.ToString());
					Print("l2",l2.ToString());
				}

			}
		}

		private void Print(string Name, string Value)
		{
			Console.WriteLine(Name);
			Console.Write(Value);
		}
	}
}
