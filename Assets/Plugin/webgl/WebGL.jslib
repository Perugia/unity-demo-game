var WebGL = {
    IsMobile: function()
    {
        return Module.SystemInfo.mobile;
    },
    Hello: function () {
        window.alert("Hello, world!");
    },
    RequestFullscreen: function () {
        if(document.fullscreenElement) 
        {
            if(document.exitFullscreen) {
                document.exitFullscreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else if (document.webkitExitFullscreen) {
                document.webkitExitFullscreen();
            }
        }
        else
        {
            let element = document.querySelector("body");
            if(element.requestFullscreen) {
                element.requestFullscreen();
            }else if (element.mozRequestFullScreen) {
                element.mozRequestFullScreen();     // Firefox
            }else if (element.webkitRequestFullscreen) {
                element.webkitRequestFullscreen();  // Safari
            }else if(element.msRequestFullscreen) {
                element.msRequestFullscreen();      // IE/Edge
            }
        }
    }
};

mergeInto(LibraryManager.library, WebGL);