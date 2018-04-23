var DXagent = navigator.userAgent.toLowerCase();
var DXopera = (DXagent.indexOf("opera") > -1);
var DXsafari = DXagent.indexOf("safari") > -1;
var DXie = (DXagent.indexOf("msie") > -1 && !DXopera);
var DXns = (DXagent.indexOf("mozilla") > -1 || DXagent.indexOf("netscape") > -1 || DXagent.indexOf("firefox") > -1) && !DXsafari && !DXie && !DXopera;
var resizeTimeout = null;

function DXattachEventToElement(element, eventName, func) {
    if(element) {
        if(DXns || DXsafari)
            element.addEventListener(eventName, func, true);
        else {
            if(eventName.toLowerCase().indexOf("on") != 0)
                eventName = "on" + eventName;
            element.attachEvent(eventName, func);
        }
    }
}
function DXGetElement(id) {
    if(document.getElementById != null) {
        return document.getElementById(id);
    }
    if(document.all != null) {
        return document.all[id];
    }
    if(document.layers != null) {
        return document.layers[id];
    }
    return null;
}
function DXGetElementHeight(id) {
    var el = DXGetElement(id);
    if(el) {
        return parseInt(el.offsetHeight);
    }
    return 0;
}
function DXGetWindowHeight() {
    var height = 0;
    if(typeof(window.innerHeight) == 'number') {
        height = window.innerHeight;
    } else if(document.documentElement && document.documentElement.clientHeight) {
        height = document.documentElement.clientHeight;
    } else if(document.body && document.body.clientHeight) {
        height = document.body.clientHeight;
    }
    var margin = 0;
    if(document.body.currentStyle) {
        margin = parseInt(document.body.currentStyle.margin);
    }
    return parseInt(height) - (margin * 2);
}
function DXGetWindowWidth() {
    var width = 0;
    if(typeof(window.clientWidth) == 'number') {
        width = window.clientWidth;
    } else if(document.documentElement && document.documentElement.clientWidth) {
        width = document.documentElement.clientWidth;
    } else if(document.body && document.body.clientWidth) {
        width = document.body.clientWidth;
    }
    var margin = 0;
    if(document.body.currentStyle) {
        margin = parseInt(document.body.currentStyle.margin);
    }
    return parseInt(width) - (margin * 2);
}
function DXMoveFooter() {
    DXUpdateSplitterSize();
    resizeTimeout = null;
}
function DXUpdateSplitterSize() {
    if(window.splitter) {
        DXUpdateSplitterWidth();
        DXUpdateSplitterHeight();
        if(!__aspxIE) {
            DXUpdateSplitterWidth();
        }
    }
}
function DXUpdateSplitterHeight() {
    var leftPaneContentHeight = DXGetElementHeight('LeftPane');
    var rightPaneContentHeight = DXGetElementHeight('ContentPane');
    var maxPaneContentHeight = (leftPaneContentHeight > rightPaneContentHeight ? leftPaneContentHeight : rightPaneContentHeight);
    var currentSplitterHeight = splitter.GetHeight();

    var bodyVisibleHeight = document.body.offsetHeight;
    var splitterHeightForSmallContent = DXGetWindowHeight() - (bodyVisibleHeight - currentSplitterHeight) - GetBrowserDependentAdditionalHeight();

    var newSplitterHeight = splitterHeightForSmallContent > maxPaneContentHeight ? splitterHeightForSmallContent : maxPaneContentHeight;
    if(currentSplitterHeight != newSplitterHeight) {
        splitter.SetHeight(newSplitterHeight);
    }
}
function DXUpdateSplitterWidth() {
    splitter.SetWidth(DXGetWindowWidth());
    if (this.mainMenu) {
        mainMenu.SetWidth(0);
    }
    var paneElement = splitter.GetPaneByName('Content').helper.GetContentContainerElement();
    var paneContentWidth = paneElement.scrollWidth;
    var visiblePaneContentWidth = paneElement.offsetWidth;
    if(paneContentWidth != visiblePaneContentWidth) {
        splitter.SetWidth(splitter.GetWidth() + paneContentWidth - visiblePaneContentWidth) - GetBrowserDependentAdditionalWidth();
    }
    if(this.mainMenu) {
        mainMenu.SetWidth(splitter.GetPaneByName("Content").GetClientWidth())
    }
}
function GetBrowserDependentAdditionalHeight() {
    if(__aspxOpera) {
        return 19;
    }
    if(__aspxChrome || __aspxFirefox) {
        return 17;
    }
    return 0;
}
function GetBrowserDependentAdditionalWidth() {
    if(__aspxOpera) {
        return 0;
    }
    if(__aspxChrome) {
        return 17;
    }
    return 0;
}
function SupressApplyingScrollPosition() {
    splitter.ApplyScrollPosition = function() { return; };
    splitter.GetPaneByName("Left").ApplyScrollPosition = function() { return; };
    splitter.GetPaneByName("Content").ApplyScrollPosition = function() { return; };
    splitter.UpdatePanesVisible = function() { return; };
}
function DXWindowOnResize(evt) {
    if(resizeTimeout) {
        window.clearTimeout(resizeTimeout);
    }
    resizeTimeout = window.setTimeout(DXMoveFooter, 100);
}
