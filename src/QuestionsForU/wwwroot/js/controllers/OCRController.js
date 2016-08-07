function OCRController($scope, service) {
    var self = this,
        canvas = null,
        context = null,
        qcanvas = null,
        qcontext = null,
        vscale = 1,
        hscale = 1,
        scale = 1,
        lines = null,
        lineno = 0,
        qscale = 1,
        selectedImage,
        ints = [],
        qheight = 32,
        qwidth = 1;;

    Init();

    $scope.$watch(function() {
            return service.ints;
        },
        function() {

            if (service.ints != null && service.ints.length > 0) {

                //qocr.Narrow(2);
                //qcontext.setTransform(1, 0, 0, 1, 0, 0); // RESET 
                //qcontext.putImageData(qocr.data, 0, 0);
                //console.log(qocr.ints); 
                console.log(service.ints);

                var qdata = qcontext.getImageData(0, 0, qcanvas.width, qcanvas.height);
                var qocr = new OCRCanvas(qdata);

                qocr.PlotCanvas(service.ints);
                qcontext.putImageData(qocr.data, 0, 0);
                self.ints = qocr.ints;
                self.qheight = qcanvas.height;
                self.qwidth = qcanvas.width;
            }
        });

    function Init() {
        canvas = $("#ocrCanvas")[0];
        context = canvas.getContext("2d");
        context.clearRect(0, 0, canvas.width, canvas.height);

        qcanvas = $("#ocrQueue")[0];
        qcontext = qcanvas.getContext("2d");
    }

    self.LoadCanvas = function(img) {
        selectedImage = $('#' + img)[0];
        //img.crossOrigin = "Anonymous";
        ScaleToCanvas();

        var data = context.getImageData(0, 0, canvas.width, canvas.height);

        var ocr = new OCRCanvas(data);
        context.putImageData(ocr.data, 0, 0);
        lines = ocr.HLines();

        ScaleToQCanvas();
        self.LoadQuiue(1);

        //console.log(ocr.height);
        //console.log(ocr.width);
        //console.log(ocr.data);
        //console.log(ocr.bits);
        //console.log(ocr.HProjection);
        //console.log(ocr.VProjection);
        //console.log(lines);
    }

    ScaleToCanvas = function() {
        context.setTransform(1, 0, 0, 1, 0, 0); // RESET 
        vscale = canvas.height / selectedImage.naturalHeight;
        hscale = canvas.width / selectedImage.naturalWidth;

        if (vscale < hscale) {
            scale = vscale;
        } else {
            scale = hscale;
        }

        context.scale(scale, scale);
        context.drawImage(selectedImage, 0, 0);
        context.save();
    }

    ScaleToQCanvas = function() {
        qcontext.setTransform(1, 0, 0, 1, 0, 0); // RESET 
        qcontext.clearRect(0, 0, qcanvas.width, qcanvas.height);
        qcanvas.width = canvas.width * r / scale;
    }

    self.LoadQuiue = function(i) {
        if (i < 0 && lineno == 1) {
            lineno = lines.length + 1;
        }

        if (i > 0 && lineno >= lines.length) {
            vice
            lineno = 0;
        }

        lineno = lineno + i;
        qcontext.clearRect(0, 0, qcanvas.width, qcanvas.height);


        var h = lines[lineno - 1].End - lines[lineno - 1].Start;
        var r = qcanvas.height / h;
        qcanvas.width = canvas.width * r;
        qcontext.scale(r * scale, r * scale);

        var l = -1 * lines[lineno - 1].Start / scale;
        qcontext.drawImage(selectedImage, 0, l);

        qcontext.save();

        var qdata = qcontext.getImageData(0, 0, qcanvas.width, qcanvas.height);
        var qocr = new OCRCanvas(qdata);

        //console.log(service.Name);
        service.$post(qocr.ints);

    }

    self.ZoomQueue = function(increase) {
        if (increase) {
            qcanvas.height = qcanvas.height * 2;
        } else {
            qcanvas.height = parseInt(qcanvas.height / 2);
        }


        self.LoadQuiue(0);
    }
}