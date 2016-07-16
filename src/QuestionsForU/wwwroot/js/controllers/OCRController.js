function OCRController($scope){
    var self = this,
    canvas=null,
    context=null,
    qcanvas = null,
    qcontext = null;
    img=null,
    ocr= null,
    rawData=null,
    VScale = 1,
    HScale = 1,
    Scale =1,
    lines = null,
    lineno = 0,
    qscale = 1;
    
    self.LoadCanvas = function () {
        canvas = $("#ocrCanvas")[0];
        context = canvas.getContext("2d");
        context.clearRect(0, 0, canvas.width, canvas.height);
        
        qcanvas = $("#ocrQueue")[0];
        qcontext = qcanvas.getContext("2d");
        
        img = $("#sourceImage")[0];
        
        img.crossOrigin = "Anonymous";
        ScaleImage();
        
        context.drawImage(img,0,0);
        
        context.save();
        
        rawData=context.getImageData(0, 0, canvas.width, canvas.height);
        ocr = new OCR(rawData);
        context.putImageData(ocr.data, 0, 0);
        lines = ocr.HLines();
        self.LoadQuiue(1);
        //console.log(ocr.height);
        //console.log(ocr.width);
        //console.log(ocr.data);
        //console.log(ocr.bits);
        //console.log(ocr.HProjection);
        //console.log(ocr.VProjection);
        //console.log(lines);
       
        //GrayScale();
        /*
                context.translate(translatePos.x, translatePos.y);
                context.scale(scale, scale);
                context.beginPath(); // begin custom shape
                context.moveTo(-119, -20);
                context.bezierCurveTo(-159, 0, -159, 50, -59, 50);
                context.bezierCurveTo(-39, 80, 31, 80, 51, 50);
                context.bezierCurveTo(131, 50, 131, 20, 101, 0);
                context.bezierCurveTo(141, -60, 81, -70, 51, -50);
                context.bezierCurveTo(31, -95, -39, -80, -39, -50);
                context.bezierCurveTo(-89, -95, -139, -80, -119, -20);
                context.closePath(); // complete custom shape
                var grd = context.createLinearGradient(-59, -100, 81, 100);
                grd.addColorStop(0, "#8ED6FF"); // light blue
                grd.addColorStop(1, "#004CB3"); // dark blue
                context.fillStyle = grd;
                context.fill();

                context.lineWidth = 5;
                context.strokeStyle = "#0000ff";
                context.stroke();
                context.restore();
       */
    }
    
    ScaleImage = function(){
        context.setTransform(1, 0, 0, 1, 0, 0); // RESET 
        VScale = canvas.height / img.naturalHeight;
        HScale = canvas.width / img.naturalWidth;
        
        Scale = (VScale > HScale) ? HScale : VScale;
        
        context.scale(Scale,Scale); 
    }
    
   
    
    self.LoadQuiue = function(i){
        if(i< 0 && lineno == 1){ // TODO
             lineno = lines.length+1;
        }
        
        if(i > 0 && lineno >= lines.length ){
             lineno = 0;
        }
         
        lineno = lineno+i;
        qcontext.setTransform(1, 0, 0, 1, 0, 0); // RESET 
        qcontext.clearRect(0, 0, qcanvas.width, qcanvas.height);
        
        var h = lines[lineno-1].End - lines[lineno-1].Start;
        var s = qcanvas.height * Scale / h;
        qcanvas.width = (canvas.width * s)/Scale;
        qcontext.scale(s,s); 
        qscale = s;
        qcontext.drawImage(img,0,-1*(lines[lineno-1].Start)/Scale + 2);
        //qcontext.putImageData(ocr.data, 0,-1*(lines[lineno-1].Start)/Scale + 2);
        
        var qrawData=qcontext.getImageData(0, 0, qcanvas.width, qcanvas.height);
        var qocr = new OCR(qrawData);
        qcontext.putImageData(qocr.data, 0, 0);
        console.log(qocr.VLines());
    }
}