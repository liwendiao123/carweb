
var lang = $("#GlobalLanguage").val();
if (lang == undefined || lang == "") {
    lang = "CN";
}
LoadLanguage = {
    Init: function () {
        /*当前语言库*/
        var _object;
        try
        {
            _object = eval("Language." + lang);
        }
        catch (error) {
            _object = undefined;
        }
        if (_object != undefined) {            
            /*label标签*/
            $("[lang_l^='l_']").each(function (i) {                
                tag = this;
                label = eval("_object." + $(tag).attr("lang_l"));
                if (label != undefined && label != "") {
                    tag.innerHTML = label;
                }
            });
            /*占位符 placeholder*/
            $("[lang_p^='p_']").each(function (i) {
                tag = this;
                placeholder = eval("_object." + $(tag).attr("lang_p"));
                if (placeholder != undefined && placeholder != "") {
                    $(tag).attr("placeholder", placeholder);
                }
            });
            /*title*/   
            $("[lang_t^='t_']").each(function (i) {
                tag = this;
                title = eval("_object." + $(tag).attr("lang_t"));
                if (title != undefined && title != "") {
                    $(tag).attr("title", title);
                }
            });
        }
        /*公共语言库*/
        var _cobject = eval("Common.Language." + lang);
        /*label标签*/
        $("[lang_l^='lc_']").each(function (i) {
            
            tag = this;
            label = eval("_cobject." + $(tag).attr("lang_l"));
            if (label != undefined && label != "") {
                tag.innerHTML = label;
            }
        });
        /*占位符 placeholder*/
        $("[lang_p^='pc_']").each(function (i) {
            tag = this;
            placeholder = eval("_cobject." + $(tag).attr("lang_p"));
            if (placeholder != undefined && placeholder != "") {
                $(tag).attr("placeholder", placeholder);
            }
        });
        /*title*/
        $("[lang_t^='tc_']").each(function (i) {
            tag = this;
            title = eval("_cobject." + $(tag).attr("lang_t"));
            if (title != undefined && title != "") {
                $(tag).attr("title", title);
            }
        });
    },
    /*根据id返回对应的语言值*/
    GetLanguageString: function (langName, defaultString) {
        if (langName == undefined || langName == "") {
            return defaultString;
        }
        var common;
        try {
            common = eval("Common.Language." + lang);
        } catch (err) {
            common = undefined;
        }
        var _object;
        if (common == undefined) {
            _object = eval("Language." + lang);
        }
        else {
            _object = jQuery.extend(eval("Common.Language." + lang), eval("Language." + lang));
        }
        str = eval("_object." + langName);
        if (str == undefined || str == "") {
            str = defaultString;
        }
        return str;
    },
    /*动态转换标签多语言*/
    ConvertLanguage: function (TagString) {
        
        var tag = $(TagString);
        var l = tag.attr("lang_l");
        if (l != "" && l != undefined)
        {
            var s = LoadLanguage.GetLanguageString(l)
            tag.html(s);
        }
        var t = $(TagString).attr("lang_t");
        if (t != "" && t != undefined) {
            var s = LoadLanguage.GetLanguageString(t)
            tag.attr("placeholder", s);
        }
        var p = $(TagString).attr("lang_p");
        if (p != "" && p != undefined) {
            var s = LoadLanguage.GetLanguageString(p)
            tag.attr("title", s);
        }
        return tag.prop("outerHTML");
    },
    Message: function (msgName, defaultMsg) {
        if (msgName == undefined || msgName == "") {
            return defaultMsg;
        }
        var prefix = msgName.substring(0, 2);
        var _object;
        if (prefix == "mc") {
            _object = eval(eval("Common.Message." + lang));
        }
        else {
            _object = eval(eval("Message." + lang));
        }
        msg = eval("_object." + msgName);
        if (msg == undefined || msg == "") {
            msg = defaultMsg;
        }
        return msg;
    }
};