using System;
using System.Collections.Generic;
using Exocortex.DSP;
using System.Threading.Tasks;

namespace QuestionsForU.OCREngine
{
    public class Retina : ConeCell
	{

		public async Task<int[]> Recognise(){
			await Task.Run(() => {
				EstimateEntity();
				ToComplexFArray();
				GenerateSignal();
				ToIntArray();
			});
			
			return Output;
		}

		private void EstimateEntity()
		{
			this.Width = (int) Math.Pow( 2, Math.Ceiling( Math.Log( Input.Length, 2 ) ) );
		}

		private void ToComplexFArray()
		{
			data = new ComplexF[this.Width * this.Height];
			for(int r=0;r<this.Width;r++){
				int d = 0;
				if(r < this.Input.Length){
					d  = Input[r];
				}
				for(int c=0;c<this.Height;c++){
					var i = r * this.Height + c;
					if((d & 1) == 1){
						data[i].Re = ((float)(0));
					}
					else{
						data[i].Re = ((float)(255));
					}
					d = d >> 1;
				}
			}
		}

		private void ToIntArray()
		{
			this.Output = new int[this.Width];
			for(int r=0;r<this.Width;r++){
				var d = 0;
				for(int c=0;c<this.Height;c++){
					var i = r * this.Height + c;
					int v = Math.Min(64, Math.Max(0, (int)(63 * data[i].GetModulus())));
					v = v / 63;
					d = d + v * ((int)Math.Pow(c, 2.0));
					
				}
				this.Output[r] = d;
			}
		}

		private void GenerateSignal()
		{
			for(int i=0;i<this.Width/this.SAMPLE_SIZE;i++){
				ForwardFFT(i);
			}
		}

		private void ForwardFFT(int period)
		{
			float scale = 1f;// 1f/this.SAMPLE_SIZE;// 1f / (float)Math.Sqrt(this.SAMPLE_SIZE * this.SAMPLE_SIZE);

			int offset = period*this.SAMPLE_SIZE;
			for (int y = 0; y < this.SAMPLE_SIZE; y++)
			{
				for (int x = (period*this.SAMPLE_SIZE); x < ((period + 1)* this.SAMPLE_SIZE); x++)
				{
					if (((x + y) & 0x1) != 0)
					{
						data[offset] *= -1;
					}
					offset++;
				}
			}

			Fourier.FFT2(data, this.SAMPLE_SIZE, this.SAMPLE_SIZE, FourierDirection.Forward);

			// May not be required
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
		}

    }
}