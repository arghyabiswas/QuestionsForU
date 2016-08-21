using Exocortex.DSP;

public class ConeCell{

        public int[] Input { protected get; set; }
        public int[] Output { get; protected set; }

        protected ComplexF[] data = null;
		public ComplexF[] Data
		{
			get { return data; }
		}

        int width = 0;
		public int Width
		{
			get 
            {
                return width; 
            }
            set
            {
                width = value;
            }
		}
		public int Height
		{
			get { return 32; }
		}

		public int SAMPLE_SIZE = 32;
		public ComplexF[] temp = null; 
}