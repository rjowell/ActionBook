class WebParser {

    run: function(arguments) {

    arguments.completionFunction({ "baseURL": window.location.href });
    }

}

var ExtensionPreprocessingJS = new WebParser;