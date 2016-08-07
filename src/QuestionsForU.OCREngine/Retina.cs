using System;
using System.Collections.Generic;
using Exocortex.DSP;
using System.Threading.Tasks;

namespace QuestionsForU.OCREngine
{
    public class Retina{
        public int[] Input { get; set; }

        public int[] Output { get; set; }

		protected ComplexF[] data = null;
		public ComplexF[] Data
		{
			get { return data; }
		}

		protected bool frequencySpace = false;
		public bool FrequencySpace
		{
			get { return frequencySpace; }
			set { frequencySpace = value; }
		}

		public int Width
		{
			get { return Input.Length; }
		}
		public int Height
		{
			get { return 32; }
		}

		public async Task<int[]> Recognise(){
			//await Task.Run(()=>
			//              Output = Input);
			Output = Input;
			//ToComplexFArray();
			//ForwardFFT();
			//ToIntArray();
			//await

			return Output;
		}

		private void ToComplexFArray()
		{
			data = new ComplexF[this.Width * this.Height];
			for (int i = 0; i < this.Width * this.Height; i++)
			{
				var pos = i % this.Width;
				if (((Input[pos] << 1) & 1) == 0)
				{
					data[i].Re = ((float)(255));
				}
				else {
					data[i].Re = ((float)(0));
				}
			}
		}

		private void ToIntArray()
		{
			for (int i = 0; i < this.Width * this.Height; i++)
			{
				var pos = i % this.Width;
				int c = Math.Min(255, Math.Max(0, (int)(256 * data[i].GetModulus())));
				c = c / 255;

				Output[pos] = Output[pos] | c;
				Output[pos] = Output[pos] >> 1;
			}
		}
		private void ForwardFFT()
		{
			float scale = 1f / (float)Math.Sqrt(this.Width * this.Height);

			int offset = 0;
			for (int y = 0; y < this.Height; y++)
			{
				for (int x = 0; x < this.Width; x++)
				{
					if (((x + y) & 0x1) != 0)
					{
						data[offset] *= -1;
					}
					offset++;
				}
			}

			Fourier.FFT2(data, this.Width, this.Height, FourierDirection.Forward);

			this.FrequencySpace = true;

			for (int i = 0; i < data.Length; i++)
			{
				data[i] *= scale;
			}


		}


		private void BackwardFFT()
		{
			float scale = 1f / (float)Math.Sqrt(this.Width * this.Height);

			Fourier.FFT2(data, this.Width, this.Height, FourierDirection.Backward);

			int offset = 0;
			for (int y = 0; y < this.Height; y++)
			{
				for (int x = 0; x < this.Width; x++)
				{
					if (((x + y) & 0x1) != 0)
					{
						data[offset] *= -1;
					}
					offset++;
				}
			}

			this.FrequencySpace = false;

			for (int i = 0; i < data.Length; i++)
			{
				data[i] *= scale;
			}
		}

		private double Correl(double[] array1,double[] array2)
		{
			
			//Two arrays
			//double[] array1 = { 3, 2, 4, 5, 6 };
			//double[] array2 = { 9, 7, 12, 15, 17 };
			 
			double[] array_xy = new double[array1.Length];
			double[] array_xp2 = new double[array1.Length];
			double[] array_yp2 = new double[array1.Length];
			for (int i = 0; i < array1.Length; i++)
			    array_xy[i] = array1[i] * array2[i];
			for (int i = 0; i < array1.Length; i++)
			    array_xp2[i] = Math.Pow(array1[i], 2.0);
			for (int i = 0; i < array1.Length; i++)
			    array_yp2[i] = Math.Pow(array2[i], 2.0);
			double sum_x = 0;
			double sum_y = 0;
			foreach (double n in array1)
			    sum_x += n;
			foreach (double n in array2)
			    sum_y += n;
			double sum_xy = 0;
			foreach (double n in array_xy)
			    sum_xy += n;
			double sum_xpow2 = 0;
			foreach (double n in array_xp2)
			    sum_xpow2 += n;
			double sum_ypow2 = 0;
			foreach (double n in array_yp2)
			    sum_ypow2 += n;
			double Ex2 = Math.Pow(sum_x, 2.00);
			double Ey2 = Math.Pow(sum_y, 2.00);
 
			double Correl = 
						(array1.Length * sum_xy - sum_x * sum_y) /
							Math.Sqrt((array1.Length * sum_xpow2 - Ex2) * (array1.Length * sum_ypow2 - Ey2));
 
			return Correl;
//Console.WriteLine("CORREL : "+ Correl);
		}

    }
}