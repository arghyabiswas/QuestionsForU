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
        //CorrectDistence(d);
        DistencePlot(d);
    }

    function DistenceMap(i) {
        var m = Math.pow(2, 31) - 1;
        var d = new Array();
        //var o = new Array();
        var rd = distences[i];
        var vsum = 0;
        d.push(0);
        //o.push(0);
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
        //o.push(0);

        distences.push(d);
        i = i + 1;
        if (vsum > 0) {
            DistenceMap(i);

            // rd : Original Array
            // d  : Distence Array 
            // distences 2d array of distences
            // distences[i] -> d

            // Finding d(1) - (d(2) + s(1)) , s(1): 1 bit covering d(2) 
            var o = angular.copy(d);
            for (c = 1; c < _width - 1; c++) {
                var c1 = o[c - 1];
                var c2 = o[c];
                var c3 = o[c + 1];

                var v11 = c1 >> 1;
                var v12 = c1;
                var v13 = c1 << 1;

                var v21 = c2 >> 1;
                var v22 = c2;
                var v23 = c2 << 1;

                var v31 = c3 >> 1;
                var v32 = c3;
                var v33 = c3 << 1;

                var v = v11 | v12 | v13 | v21 | v23 | v31 | v32 | v33;
                v = (m - v) & rd[c];

                d[c] = v | d[c];
            }

            o = angular.copy(d);
            // Finding N-S / W-E / NW-SE / NE-SW gap
            for (c = 1; c < _width - 1; c++) {
                var c1 = o[c - 1];
                var c2 = o[c];
                var c3 = o[c + 1];

                var v11 = c1 >> 1;
                var v12 = c1;
                var v13 = c1 << 1;

                var v21 = c2 >> 1;
                var v22 = c2;
                var v23 = c2 << 1;

                var v31 = c3 >> 1;
                var v32 = c3;
                var v33 = c3 << 1;

                // W-E
                var v = v12 & v32 & rd[c];

                // N-S
                v = (v21 & v23 & rd[c]) | v;

                //NW-SE
                v = (v11 & v33 & rd[c]) | v;

                //NE-SW
                v = (v13 & v31 & rd[c]) | v;

                d[c] = v | d[c];
            }
        }



        /*
        // d's Complement
        for (c = 1; c < d.length; c++) {
            d[c] = m - d[c];
            d[c] = (d[c] & rd[c]);

            d1 = (m - (d[c] & (o[c - 1] | o[c + 1]))) & d[c];
            d2 = (m - (d[c] & (o[c - 1] >> 1) | (o[c + 1] << 1))) & d[c];
            d3 = (m - (d[c] & (o[c] >> 1) | (o[c] << 1))) & d[c];
            d4 = (m - (d[c] & (o[c - 1] << 1) | (o[c + 1] >> 1))) & d[c];

            //o[c] = (m - (d[c] & o[c + 1]));

            //d[c] = (d[c] & d1 & d2 & d3 & d4);
            d[c] = (d[c] & d1);
            //d[c] = d[c] | distences[distences.length - 1][c];
        }

<<<<<<< HEAD
        */

        /*

                for (c = 1; c < d.length; c++) {
                    var result = 0;
                    var v = rd[c];
                    var v1 = d[c - 1];
                    var v2 = d[c];
                    var v3 = d[c + 1];
                    for (r = 1; r < _height - 1; r++) {
                        var r1 = v1 & Math.pow(2, (r - 1));
                        var r2 = v1 & Math.pow(2, r);
                        var r3 = v1 & Math.pow(2, (r + 1));


                        var r4 = v2 & Math.pow(2, (r - 1));
                        var r6 = v2 & Math.pow(2, (r + 1));

                        var r7 = v3 & Math.pow(2, (r - 1));
                        var r8 = v3 & Math.pow(2, r);
                        var r9 = v3 & Math.pow(2, (r + 1));

                        var b = v & Math.pow(2, r);
                        if ((r1 | r2 | r3 | r4 | r6 | r7 | r8 | r9) == 0 && b == 1) {
                            result = result + 1;
                        }
                        result = result << 1;
                    }


                    d[c] = (v2 & result) //| distences[distences.length - 1][c];;


                    //d[c] = ((m - v1) & v2 & (m - v3)) //| distences[distences.length - 1][c];
                    //d[c] = m-v2 //| distences[distences.length - 1][c];


                    //d[c] = m - ((m - (v1 >> 1)) & v2) & (d[c]);
                    //d[c] = ((m - (v1 << 1)) & v2) & (m - d[c]);
                    //d[c] = ((m - (v3 >> 1)) & v2) & (m - d[c]);
                    //d[c] = ((m - (v3 << 1)) & v2) & (m - d[c]);
                    //d[c] = !v2 & distences[distences.length - 1][c];
                    //v2 = !((v1 << 1) | v2);
                    //d[c] = !v2 & distences[distences.length - 1][c];
                    v2 = ((v3 << 1) | v2);
                    //d[c] = v2 || distences[distences.length - 1][c];
                    v2 = ((v3 << 1) | v2);
                    //d[c] = v2// | distences[distences.length - 1][c];
                    
            }
        */
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

    function CorrectDistence(d) {
        var dtop = distences[d];
        var dbottom = distences[d - 1];

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