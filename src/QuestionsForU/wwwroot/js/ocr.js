function OCR(rawData) {
    var _self = this,
        _height = 32, // TODO - Parameter
        _width = 64,
        _data = null;

    function Init(data) {
        _self._data = data;
        Start();
    }

    Init(rawData);

    OCR.prototype.data = _data;
    OCR.prototype.height = _height;
    OCR.prototype.width = _width;

    var core_queue = new Array(_self._width);
    var result_queue = new Array(_self._width);

    Start = function() {
        for (i = 0; i < _self._width; i++) {
            core_queue.push(0);
            result_queue.push(0);
        }

        for (i = 0; i < _data.length; i++) {
            Enqueue(_data[i]);
        }
    };


    Enqueue = function(item) {
        core_queue.push(item);
        ProcessDistence();
        core_queue.shift();
    };

    ProcessDistence = function() {
        for (pos = 0; pos < _width - 2; pos++) {
            if (core_queue[pos] == 0) {

            } else {
                var result = core_queue[pos] | core_queue[pos + 1] | core_queue[pos + 2];
                result_queue[pos] = result;
            }
        }
    };
}