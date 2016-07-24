function OCRCanvas(rawData) {
    var _self = this,
        _height = 1,
        _width = 1,
        THRESHOLD = 185,
        LINE_THRESHOLD = 3,
        _bitData = null,
        _data = null,
        _intData = null,
        _hProjection = null,
        _vProjection = null,
        distences = null,
        BYTE_WHITE = 255,
        BYTE_BLACK = 0;

    function Init(rawData) {
        _height = rawData.height;
        _width = rawData.width;
        _bitData = new Array();
        _hProjection = new Array();
        _vProjection = new Array();
        _intData = new Array();


        for (i = 0, n = rawData.data.length; i < n; i = i + 4) {
            var pixel = parseInt((rawData.data[i] + rawData.data[i + 1] + rawData.data[i + 2]) / 3);

            if (pixel > 0 && pixel < THRESHOLD) {
                pixel = BYTE_BLACK;
                _bitData.push(1);
            } else {
                pixel = BYTE_WHITE;
                _bitData.push(0);
            }

            rawData.data[i] = pixel;
            rawData.data[i + 1] = pixel;
            rawData.data[i + 2] = pixel;
            //rawData.data[i+3] = pixel;

        }
        _data = rawData;


        for (r = 0; r < _width; r++) {
            var _hsum = 0;
            var _val = 0;
            for (c = 0; c < _height; c++) {
                var v = _bitData[r * _height + c];
                _val = _val << 1;
                if (v == 1) {
                    _hsum++;

                }
                if (_bitData[c * _width + r] == 1) {
                    _val++;
                }
            }
            _vProjection.push(_hsum);
            _intData.push(_val);
        }

        for (c = 0; c < _height; c++) {
            var _vsum = 0;

            for (r = 0; r < _width; r++) {
                var v = _bitData[c * _width + r];

                if (v == 1) {
                    _vsum++;

                }
            }

            _hProjection.push(_vsum);

        }

    }

    Init(rawData);

    OCRCanvas.prototype.data = _data;
    OCRCanvas.prototype.height = _height;
    OCRCanvas.prototype.width = _width;
    OCRCanvas.prototype.bits = _bitData;
    OCRCanvas.prototype.HProjection = _hProjection;
    OCRCanvas.prototype.VProjection = _vProjection;
    OCRCanvas.prototype.ints = _intData;
    OCRCanvas.prototype.HLines = function() {
        var lines = new Array();
        var dots = 0;
        var start = 0;
        var end = 0;
        for (i = 0, n = _hProjection.length; i < n; i++) {
            if (_hProjection[i] == 0) {
                dots++;
                if (start > 0) {
                    end = i + 1; // Use One blank bit
                    lines.push({
                        'Start': start,
                        'End': end
                    });
                    start = 0;
                    end = 0;
                }
            } else {
                if (dots >= LINE_THRESHOLD) {
                    start = i - 1; // Use one blank bit
                }

                dots = 0;
            }
        }

        return lines;
    }

    OCRCanvas.prototype.VLines = function() {
        var lines = new Array();
        var dots = 0;
        var start = 0;
        var end = 0;
        for (i = 0, n = _vProjection.length; i < n; i++) {
            if (_vProjection[i] == 0) {
                dots++;
                if (start > 0) {
                    end = i + 1; // Use One blank bit
                    lines.push({
                        'Start': start,
                        'End': end
                    });

                    start = 0;
                    end = 0;
                }
            } else {
                if (dots >= LINE_THRESHOLD) {
                    start = i - 1; // Use one blank bit
                }

                dots = 0;
            }
        }

        return lines;
    }



    OCRCanvas.prototype.Narrow = function(d) {
        distences = new Array();
        distences.push(_intData);
        DistenceMap(0);
        /*
        console.log(distences.length);
        for (i = 0; i < distences.length; i++) {
            console.log(distences[i]);
        }
        */
        CorrectDistence(d);
        DistencePlot(d);
    }

    function DistenceMap(i) {
        var d = new Array();
        var rd = distences[i];
        var vsum = 0;
        d.push(0);
        for (c = 1; c < _width - 1; c++) {
            var pos = c * _height;
            var v2 = rd[c - 1] & rd[c] & rd[c + 1];
            var v1 = v2 >> 1;
            var v3 = v2 << 1;
            var v = v2 & v1 & v3;
            d.push(v);
            vsum = vsum | v;
        }
        d.push(0);

        distences.push(d);
        i = i + 1;
        if (vsum > 0) {
            DistenceMap(i);
        }
    }

    function DistencePlot(d) {
        _intData = distences[d - 1];
        OCRCanvas.prototype.ints = _intData;
        for (i = 0, n = _data.data.length; i < n; i = i + 4) {
            var pos = parseInt(i % (_width * 4)) / 4;
            var h = parseInt(i / (_width * 4));
            var pixel = BYTE_WHITE;
            var v = _intData[pos];
            var b = Math.pow(2, (_height - h));
            if ((v & b) == 0) {
                pixel = BYTE_WHITE;
            } else {
                pixel = BYTE_BLACK;
            }

            _data.data[i] = pixel;
            _data.data[i + 1] = pixel;
            _data.data[i + 2] = pixel;
            //_data.data[i+3] = pixel;

        }

    }

    function CorrectDistence(d){
        var dtop = distences[d];
        var dbottom = distences[d-1];

        for (c = 1; c < _width - 1; c++) {
            var pos = c * _height;
            var v2 = rd[c - 1] & rd[c] & rd[c + 1];
            var v1 = v2 >> 1;
            var v3 = v2 << 1;
            var v = v2 & v1 & v3;
            d.push(v);
            vsum = vsum | v;
        }
    }
}