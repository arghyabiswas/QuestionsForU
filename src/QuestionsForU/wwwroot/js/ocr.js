function OCR(rawData) {
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

    OCR.prototype.data = _data;
    OCR.prototype.height = _height;
    OCR.prototype.width = _width;
    OCR.prototype.bits = _bitData;
    OCR.prototype.HProjection = _hProjection;
    OCR.prototype.VProjection = _vProjection;
    OCR.prototype.ints = _intData;
    OCR.prototype.HLines = function() {
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

    OCR.prototype.VLines = function() {
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
}