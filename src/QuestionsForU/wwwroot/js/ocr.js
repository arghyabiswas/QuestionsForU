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
    }

    Init(rawData);

    OCR.prototype.data = _data;
    OCR.prototype.height = _height;
    OCR.prototype.width = _width;
}