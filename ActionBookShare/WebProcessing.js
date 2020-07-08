var MyExtensionJavaScriptClass = function () { };

MyExtensionJavaScriptClass.prototype = {
    run: function (arguments) {

        var images = document.getElementsByTagName("img");
        var imageList = "";

        var counter;
        if (images.length < 11) {
            counter = images.length ;
        }
        else {
            counter = 10;
        }

        var j;
        for (j = 0; j < counter; j++) {

            imageList+=images[j].src+",";
            
        }

        

        arguments.completionFunction({


            "pageTitle": document.title,
            "pageURL": document.URL,
            "imageList": imageList

        });
    },

    // Note that the finalize function is only available in iOS.

    finalize: function (arguments) {
        // arguments contains the value the extension provides in [NSExtensionContext completeRequestReturningItems:completion:].
        // In this example, the extension provides a color as a returning item.
        
    }
};

// The JavaScript file must contain a global object named "ExtensionPreprocessingJS".

var ExtensionPreprocessingJS = new MyExtensionJavaScriptClass;
