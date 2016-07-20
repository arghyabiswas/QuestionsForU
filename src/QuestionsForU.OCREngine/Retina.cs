using System;
using System.Collections.Generic;

namespace QuestionsForU.OCREngine
{
    public class Retina{
        public IEnumerable<int> Input { get; set; }

        public IEnumerable<int> Output { get; set; }

        public async void Narrow(){
            Output = Input;
        }
    }
}