﻿function downloadFromUrl(options) {
    var _a;
    var anchorElement = document.createElement('a');
    anchorElement.href = options.url;
    anchorElement.download = (_a = options.fileName) !== null && _a !== void 0 ? _a : '';
    anchorElement.click();
    anchorElement.remove();
}