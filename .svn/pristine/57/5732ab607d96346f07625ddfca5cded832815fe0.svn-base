//判断浏览器
var userAgent = navigator.userAgent, isIE = false;
if (userAgent.indexOf('MSIE') != -1) {
    isIE = true;
}

//查找对象
function MM_findObj(n, d) { //v4.0
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && document.getElementById) x = document.getElementById(n); return x;
}

//显示隐藏通知层
function MM_showHideLayers() { //v6.0
    var i, p, v, obj, args = MM_showHideLayers.arguments;
    for (i = 0; i < (args.length - 2); i += 3) if ((obj = MM_findObj(args[i])) != null) {
        v = args[i + 2];
        if (obj.style) { obj = obj.style; v = (v == 'show') ? 'visible' : (v == 'hide') ? 'hidden' : v; }
        obj.visibility = v;
    }
}

//隐藏显示层，加强版
function MM_showHideLayers_pro() { //v9.0
    var i, p, v, obj, args = MM_showHideLayers_pro.arguments;
    for (i = 0; i < (args.length - 2); i += 3)
        with (document) if (getElementById && ((obj = getElementById(args[i])) != null)) {
            v = args[i + 2];
            if (obj.style) { obj = obj.style; v = (v == 'show') ? 'block' : (v == 'hide') ? 'none' : v; }
            obj.display = v;
        }
}

//交换图象存储
function MM_swapImgRestore() { //v3.0
    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
}

//预载入图象
function MM_preloadImages() { //v3.0
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    }
}

//交换图象
function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
}

//设置控件的HTML
function SetControlHTML(obj, str) {
    MM_findObj(obj).innerHTML = str;
}

//更换控件的css样式
function ChangeClassName(obj, classname) {
    obj.className = classname;
}

//重新获取验证码
function ReGetVerifyCode(myIMG) {
    MM_findObj(myIMG).src = "/VerifyCodeImg.aspx?ID=" + MM_GetTime();
}
//重新获取验证码
function ReGetVerifyCode2(myIMG) {
    MM_findObj(myIMG).src = "/VerifyCodeImg2.aspx?ID=" + MM_GetTime();
}

//ASCII字符长度
String.prototype.len = function () {
    return this.replace(/[^x00-xff]/g, "**").length;
}

//去掉字符串头部和尾部的空格
String.prototype.trim = function () {
    //   用正则表达式将前后空格   
    //   用空字符串替代。   
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

//截取字符串前面几个字符（按英文字节算、带省略号）
String.prototype.sub = function (n) {
    var r = /[^\x00-\xff]/g;
    if (this.replace(r, "mm").length <= n) return this;
    // n = n - 3;     
    var m = Math.floor(n / 2);
    for (var i = m; i < this.length; i++) {
        if (this.substr(0, i).replace(r, "mm").length >= n) {
            return this.substr(0, i) + "...";
        }
    } return this;
};

//获取xmlhttp对象
function GetXmlHttpRequest() {
    var oBao;
    if (window.XMLHttpRequest) {
        oBao = new XMLHttpRequest();
    }
    else if (window.ActiveXObject) {
        oBao = new ActiveXObject("Microsoft.XMLHTTP");
    }
    else {
        alert("您的浏览器不支持XMLHTTP无刷新技术！");
        return;
    }
    return oBao;
}

//获取时间
function MM_GetTime() {
    var d = new Date();
    return d.getTime();
}

//添加cookie
function addCookie(objName, objValue, objHours) {//添加cookie
    var str = objName + "=" + escape(objValue);
    if (objHours > 0) {//为0时不设定过期时间，浏览器关闭时cookie自动消失
        var date = new Date();
        var ms = objHours * 3600 * 1000;
        date.setTime(date.getTime() + ms);
        str += "; expires=" + date.toGMTString();
    }
    document.cookie = str;
}

//获取指定名称的cookie的值
function getCookie(objName) {
    var arrStr = document.cookie.split("; ");
    for (var i = 0; i < arrStr.length; i++) {
        var temp = arrStr[i].split("=");
        if (temp[0] == objName) return unescape(temp[1]);
    }
}

//为了删除指定名称的cookie，可以将其过期时间设定为一个过去的时间
function delCookie(name) {
    var date = new Date();
    date.setTime(date.getTime() - 10000);
    document.cookie = name + "=a; expires=" + date.toGMTString();
}

//读取出来所有的cookie字筗串了
function allCookie() {//读取所有保存的cookie字符串
    var str = document.cookie;
    if (str == "") {
        str = "没有保存任何cookie";
    }
    alert(str);
}

//获取下一级区域下拉菜单
function GetDropDownListArea(strControlID, strSelValue) {
    try {
        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "/AjaxData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;
                    oBao.abort();
                    //创建下来菜单数组
                    var objArea = MM_findObj(strControlID);
                    //删除原有下来菜单
                    DelDropDownList(objArea);
                    //添加下来菜单
                    AddDropDownList(objArea, strText, "");
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetDropDownListArea&SelValue=" + escape(strSelValue));
    }
    catch (ex)
	{ }
}

//获取下区域下拉菜单三级联动
function GetDropDownListAreaAll(strControlID1, strControlID2, strSelValue, strSelValue1, strSelValue2) {
    try {
        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "AjaxData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;
                    oBao.abort();

                    //创建下来菜单数组
                    var objArea = MM_findObj(strControlID1);
                    //删除原有下来菜单
                    DelDropDownList(objArea);
                    //添加下来菜单
                    AddDropDownList(objArea, strText, strSelValue1);

                    //判断当没有下级数据的时候，停止递归
                    if (strControlID2 == "" || strSelValue2 == "")
                    { }
                    else
                        GetDropDownListAreaAll(strControlID2, "", strSelValue1, strSelValue2, ""); //递归读取
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetDropDownListArea&SelValue=" + escape(strSelValue));
    }
    catch (ex)
	{ }
}

//设置默认车系
function GetDropDownListCar(strControlID1, strSelValue, strSelValue1, strType) {
    //----------------------------
    try {
        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "/AjaxData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;
                    oBao.abort();
                    //创建下来菜单数组
                    var objArea = MM_findObj(strControlID1);
                    if (objArea != null) {
                        //删除原有下来菜单
                        DelDropDownList(objArea);
                        //添加下来菜单
                        AddDropDownList(objArea, strText, strSelValue1);
                    }

                    //---------------------------------                    
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetDropDownListCarSeries&SelValue=" + escape(strSelValue));
    }
    catch (ex)
	{ }
}

//获取车系
function GetDropDownListCarSeries(strControlID, strSelValue) {
    try {
        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "/AjaxData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;
                    oBao.abort();
                    //创建下来菜单数组
                    var objArea = MM_findObj(strControlID);
                    //删除原有下来菜单
                    DelDropDownList(objArea);
                    //添加下来菜单
                    AddDropDownList(objArea, strText, "");
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetDropDownListCarSeries&SelValue=" + escape(strSelValue));
    }
    catch (ex)
	{ }
}

//获取年数
function GetDropDownListCarYear(strControlID, strSelValue) {
    try {
        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "AjaxData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;
                    oBao.abort();
                    //创建下来菜单数组
                    var objArea = MM_findObj(strControlID);
                    //删除原有下来菜单
                    DelDropDownList(objArea);
                    //添加下来菜单
                    AddDropDownList(objArea, strText, "");
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetDropDownListCarYear&SelValue=" + escape(strSelValue));
    }
    catch (ex)
	{ }
}

//获取型号
function GetDropDownListCarModel(strControlID, strSelValue, strType) {
    try {
        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "AjaxData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;
                    oBao.abort();
                    //创建下来菜单数组
                    var objArea = MM_findObj(strControlID);
                    //删除原有下来菜单
                    DelDropDownList(objArea);
                    //添加下来菜单
                    AddDropDownList(objArea, strText, "");
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetDropDownListCarModel&Type=" + strType + "&SelValue=" + escape(strSelValue));
    }
    catch (ex)
	{ }
}


//删除下拉菜单
function DelDropDownList(obj) {
    while (obj.options.length > 1) {
        obj.options.remove(obj.options.length - 1);
    }
}

//添加下拉菜单
function AddDropDownList(objArea, strText, selValue) {
    //判断有没有数据
    if (strText.indexOf("|") > -1) {
        var arrData = strText.split(',');
        for (var i = 0; i < arrData.length; i++) {
            var arrOp = arrData[i].split('|');
            if (arrOp.length == 2) {
                objArea.options.add(new Option(arrOp[1], arrOp[0]));

                //判断是否选中菜单
                if (arrOp[0] == selValue) objArea.options.selectedIndex = objArea.options.length - 1;
            }
        }
    }
}

//检查用户名
function CheckUserName(source, arguments) {
    var pps = /^[a-zA-Z0-9_\u4e00-\u9fa5]{4,20}$/;
    if (!pps.test(arguments.Value.replace(/[\u4e00-\u9fa5]/g, "11")))
        arguments.IsValid = false;
    else
        arguments.IsValid = true;
}

//检查密码是否一致
function CheckPassWord(source, arguments) {
    if (MM_findObj("tbPassWord").value != arguments.Value)
        arguments.IsValid = false;
    else
        arguments.IsValid = true;
}

//是否选择市
function CheckArea1(source, arguments) {
    var tempObj = MM_findObj("ddlArea1");
    if (tempObj.options[tempObj.selectedIndex].value == "0") {
        arguments.IsValid = false;
    }
    else
        arguments.IsValid = true;
}

//设置二手车标题
function SetCarTitle() {
    var obj1 = MM_findObj("ddlCarBrands");
    var obj2 = MM_findObj("ddlCarSeries");
    //var obj3 = MM_findObj("ddlCarYear");
    //var obj4 = MM_findObj("ddlCarModel");
    var obj5 = MM_findObj("tbTitle");
    var obj6 = MM_findObj("tbKeyWords");
    var str1, str2, str3, str4;
    str1 = obj1.options[obj1.selectedIndex].text;
    str2 = obj2.options[obj2.selectedIndex].text;
    //str3 = obj3.options[obj3.selectedIndex].text;
    //str4=obj4.options[obj4.selectedIndex].text;
    for (var i = 65; i < 91; i++) {
        str1 = str1.replace(String.fromCharCode(i) + " ", "");
        str2 = str2.replace(String.fromCharCode(i) + " ", "");
    }

    obj5.value = str2;
    obj6.value = str1 + " " + str2;
}

//验证是否选择销售人员或是否添加新销售人员
function CheckSales(source, arguments) {
    var tempObj = MM_findObj("ddlSales");
    if (tempObj == null) {
        if (MM_findObj("tbSaleFullName").value.trim() == "" || MM_findObj("tbSaleMobile").value.trim() == "" || MM_findObj("tbSaleQQ").value.trim() == "") {
            MM_findObj("tbSaleFullName").focus();
            arguments.IsValid = false;
        }
        else
            arguments.IsValid = true;
    }
    else {
        if (tempObj.options[tempObj.selectedIndex].value == "0") {
            if (MM_findObj("tbSaleFullName").value.trim() == "" || MM_findObj("tbSaleMobile").value.trim() == "" || MM_findObj("tbSaleQQ").value.trim() == "")
                arguments.IsValid = false;
            else
                arguments.IsValid = true;
        }
        else {
            arguments.IsValid = true;
        }
    }
}

//验证封面图片是否上传
function CheckBigPic(source, arguments) {
    var tempObj = MM_findObj("hfBigPic");
    if (tempObj == null) {
        arguments.IsValid = false;
    }
    else {
        if (tempObj.value == "") {
            arguments.IsValid = false;
        }
        else {
            arguments.IsValid = true;
        }
    }
}

//验证是否选中一个
function CheckCarTypeSelected(source, arguments) {
    var isSelected = false;
    var checkbox = document.all.tags("input");
    for (var i = 0; i < checkbox.length; i++) {
        if (checkbox[i].type == "checkbox") {
            if (checkbox[i].name.indexOf("cblCarType") > -1)
                if (checkbox[i].checked) isSelected = true;
        }
    }
    arguments.IsValid = isSelected;
}

//选择已过期内容
function ResetSelected(obj1, obj2) {
    if (obj1.options[obj1.selectedIndex].value == '-') {
        obj2.selectedIndex = 0;
    }
}

//设置显示或不显示数据
function SetShowOrFalse(plobj, TypeID, v, id) {
    try {
        //SetControlHTML(plobj, "正在生成静态页面，请稍候.....");

        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "UpdateData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;

                    if (strText == "0") {
                        plobj.innerHTML = "<FONT color=red>×</FONT>";
                        plobj.onclick = function () { SetShowOrFalse(plobj, TypeID, strText, id) };
                    }
                    else {
                        plobj.innerHTML = "<FONT color=blue>√</FONT>";
                        plobj.onclick = function () { SetShowOrFalse(plobj, TypeID, strText, id) };
                    }
                    oBao.abort();
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=SetShowOrFalse&typeid=" + escape(TypeID) + "&v=" + escape(v) + "&id=" + id + "&ssss=" + MM_GetTime());
    }
    catch (ex)
	{ }
}

var currPage = 2;
//获取下一页车源列表信息
function GetCarList(plobj, plobj1, page) {
    try {
        SetControlHTML(plobj, "<img src='Content/Images/loading.gif' />");

        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "Mobile/GetList", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    currPage++;
                    strText = oBao.responseText;
                    MM_findObj(plobj1).innerHTML = MM_findObj(plobj1).innerHTML + strText;
                    oBao.abort();
                    if (strText == "")
                        SetControlHTML(plobj, "<input id='iMove' type=\"button\" value=\"没有了\" />");
                    else
                        SetControlHTML(plobj, "<input type=\"button\" onclick=\"GetCarList('" + plobj + "','" + plobj1 + "'," + (currPage) + ")\" value=\"按住上拉查看更多\" />");
                }
                else
                    SetControlHTML(plobj, "<input type=\"button\" onclick=\"GetCarList('" + plobj + "','" + plobj1 + "'," + (currPage) + ")\" value=\"按住上拉查看更多\" />");
            }
            else
                SetControlHTML(plobj, "<input type=\"button\" onclick=\"GetCarList('" + plobj + "','" + plobj1 + "'," + (currPage) + ")\" value=\"按住上拉查看更多\" />");
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetList&page=" + escape(page) + "&CarType=" + $("#CarType").val()
        + "&HopePrice=" + $("#HopePrice").val() + "&UseYears=" + $("#HopePrice").val() + "&Search=" + $("#Search").val() + "&ssss=" + MM_GetTime());
    }
    catch (ex)
	{ alert(ex); }
}

//获取下一页车源列表信息
function GetCarList1(plobj, plobj1, page) {
    try {
        SetControlHTML(plobj, "<img src='/Content/Images/loading.gif' />");

        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "/Mobile/GetMyList", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    currPage++;
                    strText = oBao.responseText;
                    MM_findObj(plobj1).innerHTML = MM_findObj(plobj1).innerHTML + strText;
                    oBao.abort();
                    if (strText == "")
                        SetControlHTML(plobj, "<input id='iMove' type=\"button\" value=\"没有了\" />");
                    else
                        SetControlHTML(plobj, "<input type=\"button\" onclick=\"GetCarList1('" + plobj + "','" + plobj1 + "'," + (currPage) + ")\" value=\"按住上拉查看更多\" />");
                }
                else
                    SetControlHTML(plobj, "<input type=\"button\" onclick=\"GetCarList1('" + plobj + "','" + plobj1 + "'," + (currPage) + ")\" value=\"按住上拉查看更多\" />");
            }
            else
                SetControlHTML(plobj, "<input type=\"button\" onclick=\"GetCarList1('" + plobj + "','" + plobj1 + "'," + (currPage) + ")\" value=\"按住上拉查看更多\" />");
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetMyList&page=" + escape(page) + "&CarType=" + $("#CarType").val()
        + "&HopePrice=" + $("#HopePrice").val() + "&UseYears=" + $("#HopePrice").val() + "&Search=" + $("#Search").val() + "&ssss=" + MM_GetTime());
    }
    catch (ex)
    { alert(ex); }
}
//获取下一页车源列表信息
function GetCarList2(plobj, plobj1, page) {
    try {
        SetControlHTML(plobj, "<img src='/Content/Images/loading.gif' />");

        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "/Mobile/GetAuditList", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    currPage++;
                    strText = oBao.responseText;
                    MM_findObj(plobj1).innerHTML = MM_findObj(plobj1).innerHTML + strText;
                    oBao.abort();
                    if (strText == "")
                        SetControlHTML(plobj, "<input id='iMove' type=\"button\" value=\"没有了\" />");
                    else
                        SetControlHTML(plobj, "<input type=\"button\" onclick=\"GetCarList2('" + plobj + "','" + plobj1 + "'," + (currPage) + ")\" value=\"按住上拉查看更多\" />");
                }
                else
                    SetControlHTML(plobj, "<input type=\"button\" onclick=\"GetCarList2('" + plobj + "','" + plobj1 + "'," + (currPage) + ")\" value=\"按住上拉查看更多\" />");
            }
            else
                SetControlHTML(plobj, "<input type=\"button\" onclick=\"GetCarList2('" + plobj + "','" + plobj1 + "'," + (currPage) + ")\" value=\"按住上拉查看更多\" />");
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetAuditList&page=" + escape(page) + "&CarType=" + $("#CarType").val()
        + "&HopePrice=" + $("#HopePrice").val() + "&UseYears=" + $("#HopePrice").val() + "&Search=" + $("#Search").val() + "&ssss=" + MM_GetTime());
    }
    catch (ex)
    { alert(ex); }
}
//全选
function CheckAll() {
    var checkbox = document.all.tags("input");
    for (var i = 0; i < checkbox.length; i++) {
        if (checkbox[i].type == "checkbox") {
            if (checkbox[i].name.indexOf("cbSel") > -1) {
                if (!checkbox[i].checked)
                    checkbox[i].click();
            }
        }
    }
}

//反选
function CheckOthers() {
    var checkbox = document.all.tags("input");
    for (var i = 0; i < checkbox.length; i++) {
        if (checkbox[i].type == "checkbox") {
            if (checkbox[i].name.indexOf("cbSel") > -1)
                checkbox[i].click();
        }
    }
}

//首页推荐车型菜单切换
function ChangeCarTypeMenu(divID, plObj) {
    for (var i = 0; i < 10; i++) {
        var tempObj = MM_findObj("CarTypeDiv_" + i);
        if (tempObj != null) {
            ChangeClassName(tempObj, "index_13_1");
        }

        var tempObj1 = MM_findObj("topMenu" + i);
        if (tempObj1 != null) {
            ChangeClassName(tempObj1, "index_11");
        }
    }

    ChangeClassName(MM_findObj(divID), "index_13");
    ChangeClassName(plObj, "index_11_1");
}

//切换地区菜单
function ChangeArea(objID) {
    var obj = MM_findObj(objID);
    if (obj.className == 'SelectArea')
        obj.className = 'SelectArea1';
    else
        obj.className = 'SelectArea';
}

//设置显示或不显示数据
function GetSaleMobile(plobj, strSelValue) {
    try {
        SetControlHTML(plobj, "正在获取信息，请稍候.....");

        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "AjaxData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;

                    oBao.abort();

                    SetControlHTML(plobj, "<b>" + strText + "</b>");
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetSaleMobile&SelValue=" + strSelValue + "&ssss=" + MM_GetTime());
    }
    catch (ex)
	{ }
}

//验证发布二手车，车型
function CheckCarAddCarBrands(source, arguments) {
    var obj1 = MM_findObj("ddlCarBrands");
    var obj2 = MM_findObj("ddlCarSeries");
    //var obj3=MM_findObj("ddlCarYear");
    //var obj4 = MM_findObj("ddlCarModel");
    if (obj1 != null && obj2 != null) {
        if (obj1.options[obj1.selectedIndex].value != "0" && obj2.options[obj2.selectedIndex].value != "0")
            arguments.IsValid = true;
        else
            arguments.IsValid = false;
    }
    else
        arguments.IsValid = false;
}

//验证发布二手车，登记日期
function CheckCarAddRegDate(source, arguments) {
    var strIsRegDate = document.getElementById("chbIsRegDate_0");
    if (strIsRegDate.checked) {
        arguments.IsValid = true;
    }
    else {
        var obj1 = MM_findObj("ddlRegDateYear");
        var obj2 = MM_findObj("ddlRegDateMonth");
        if (obj1 != null && obj2 != null) {
            if (obj1.options[obj1.selectedIndex].value != "0" && obj2.options[obj2.selectedIndex].value != "0")
                arguments.IsValid = true;
            else
                arguments.IsValid = false;
        }
        else
            arguments.IsValid = false;
    }
}

//验证发布二手车，登记地区
function CheckCarAddNoteArea(source, arguments) {
    var obj1 = MM_findObj("ddlNoteArea");
    var obj2 = MM_findObj("ddlNoteArea1");
    if (obj1 != null && obj2 != null) {
        if (obj1.options[obj1.selectedIndex].value != "0" && obj2.options[obj2.selectedIndex].value != "0")
            arguments.IsValid = true;
        else
            arguments.IsValid = false;
    }
    else
        arguments.IsValid = false;
}

//验证发布二手车，交易地区
function CheckCarAddUserArea(source, arguments) {
    var obj1 = MM_findObj("ddlUserArea");
    var obj2 = MM_findObj("ddlUserArea1");
    if (obj1 != null && obj2 != null) {
        if (obj1.options[obj1.selectedIndex].value != "0" && obj2.options[obj2.selectedIndex].value != "0")
            arguments.IsValid = true;
        else
            arguments.IsValid = false;
    }
    else
        arguments.IsValid = false;
}

//验证发布二手车，年审
function CheckCarAddAnnualDate(source, arguments) {
    var obj1 = MM_findObj("ddlAnnualDateYear");
    var obj2 = MM_findObj("ddlAnnualDateMonth");
    if (obj1 != null && obj2 != null) {
        if (obj1.options[obj1.selectedIndex].value == "-") {
            arguments.IsValid = true;
        }
        else if (obj1.options[obj1.selectedIndex].value != "0" && obj2.options[obj2.selectedIndex].value != "0")
            arguments.IsValid = true;
        else
            arguments.IsValid = false;
    }
    else
        arguments.IsValid = false;
}

//验证发布二手车，交强险
function CheckCarAddEnforceDate(source, arguments) {
    var obj1 = MM_findObj("ddlEnforceDateYear");
    var obj2 = MM_findObj("ddlEnforceDateMonth");
    if (obj1 != null && obj2 != null) {
        if (obj1.options[obj1.selectedIndex].value == "-") {
            arguments.IsValid = true;
        }
        else if (obj1.options[obj1.selectedIndex].value != "0" && obj2.options[obj2.selectedIndex].value != "0")
            arguments.IsValid = true;
        else
            arguments.IsValid = false;
    }
    else
        arguments.IsValid = false;
}

//删除车辆图片
function DelCarPis(strSelValue, strPicName, strImg1, strInput1) {
    if (confirm("确认删除选中图片？")) {
        try {
            var oBao = GetXmlHttpRequest();
            oBao.open("POST", "AjaxData.aspx", true);
            oBao.onreadystatechange = function () {
                if (oBao.readyState == 4) {
                    if (oBao.status == 200) {
                        strText = oBao.responseText;
                        oBao.abort();

                        if (strText == "1") {
                            MM_findObj(strImg1).src = "images/tu_03.jpg";
                            MM_findObj(strInput1).src = "";
                            alert("删除成功！");
                        }
                        else {
                            MM_findObj(strImg1).src = "images/tu_03.jpg";
                            MM_findObj(strInput1).src = "";
                            alert("删除失败！可能登录超时或已经被删除！");
                        }
                    }
                }
            }
            oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            oBao.send("Action=DelCarPic&SelValue=" + escape(strSelValue) + "&PicName=" + escape(strPicName));
        }
        catch (ex)
	{ }
    }
}

//显示隐藏DIV
function ShowConfigDiv(obj) {
    var tempObj = MM_findObj(obj);
    if (tempObj != null) {
        if (tempObj.className == "configDiv1")
            tempObj.className = "configDiv";
        else
            tempObj.className = "configDiv1";
    }
}

//添加配置
function AddConfiguration(obj) {
    var tempObj = MM_findObj("tbConfiguration");
    if (tempObj != null) {
        if (("," + tempObj.value).indexOf(obj.value) > -1) {
            if (!obj.checked) {
                if (tempObj.value.indexOf(obj.value.replace(",", "")) > 0)
                    tempObj.value = tempObj.value.replace(obj.value, "");
                else
                    tempObj.value = tempObj.value.replace(obj.value.replace(",", ""), "");
            }
        }
        else {
            if (obj.checked) {
                if (tempObj.value == "")
                    tempObj.value = tempObj.value + obj.value.replace(",", "");
                else
                    tempObj.value = tempObj.value + obj.value;
            }

        }
    }
}

//添加保修
function AddWarranty(obj) {
    var tempObj = MM_findObj("tbWarranty");
    if (tempObj != null) {
        if (("," + tempObj.value).indexOf(obj.value) > -1) {
            if (!obj.checked) {
                if (tempObj.value.indexOf(obj.value.replace(",", "")) > 0)
                    tempObj.value = tempObj.value.replace(obj.value, "");
                else
                    tempObj.value = tempObj.value.replace(obj.value.replace(",", ""), "");
            }
        }
        else {
            if (obj.checked) {
                if (tempObj.value == "")
                    tempObj.value = tempObj.value + obj.value.replace(",", "");
                else
                    tempObj.value = tempObj.value + obj.value;
            }

        }
    }
}

//菜单子类折叠
function ShowHideSubMenu(imgObj, plobj) {
    if (imgObj.src.indexOf("images/admin_jxs_about_02.jpg") > -1) {
        MM_findObj(plobj).style.display = 'none';
        imgObj.src = "images/admin_jxs_about_03.jpg";
    }
    else {
        MM_findObj(plobj).style.display = 'block';
        imgObj.src = "images/admin_jxs_about_02.jpg";
    }
}

//排行菜单切换
function CPL(type) {
    if (type == "1") {
        MM_findObj("ph1").className = "index_27";
        MM_findObj("ph2").className = "index_26";

        MM_findObj("phDiv1").style.display = "block";
        MM_findObj("phDiv2").style.display = "none";
    }
    else {
        MM_findObj("ph1").className = "index_26";
        MM_findObj("ph2").className = "index_27";

        MM_findObj("phDiv1").style.display = "none";
        MM_findObj("phDiv2").style.display = "block";

    }
}

//获取排名数据
function GetPHB(strSelValue, type) {
    try {
        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "AjaxData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;
                    oBao.abort();

                    var arrText = new Array();
                    arrText = strText.split("|");
                    for (var i = 0; i < 10; i++) {
                        if (i < arrText.length)
                            MM_findObj("ph" + type + "Text" + i).innerHTML = arrText[i];
                        else
                            MM_findObj("ph" + type + "Text" + i).innerHTML = "";
                    }
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetPHB&SelValue=" + escape(strSelValue) + "&type=" + escape(type));
    }
    catch (ex)
	{ }
}

//获取参加人数
function GetEventsUserNum(plObj, strSelValue) {
    try {
        var oBao = GetXmlHttpRequest();
        oBao.open("POST", "AjaxData.aspx", true);
        oBao.onreadystatechange = function () {
            if (oBao.readyState == 4) {
                if (oBao.status == 200) {
                    strText = oBao.responseText;
                    oBao.abort();

                    SetControlHTML(plObj, strText);
                }
            }
        }
        oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oBao.send("Action=GetEventsUserNum&SelValue=" + escape(strSelValue));
    }
    catch (ex)
	{ }
}

//判断用户名是否存在
function CheckUserNameExists(plObj, strSelValue) {

    if (strSelValue.trim() == "")
        SetControlHTML(plObj, "请输入用户名！");
    else {
        try {
            var oBao = GetXmlHttpRequest();
            oBao.open("POST", "AjaxData.aspx", true);
            oBao.onreadystatechange = function () {
                if (oBao.readyState == 4) {
                    if (oBao.status == 200) {
                        strText = oBao.responseText;
                        oBao.abort();

                        if (strText == "1")
                            SetControlHTML(plObj, "该用户名已经被注册！请选择一个另外的用户名！");
                        else
                            SetControlHTML(plObj, "");
                    }
                }
            }
            oBao.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            oBao.send("Action=CheckUserName&SelValue=" + escape(strSelValue));
        }
        catch (ex)
	    { }
    }
}

//网站下拉菜单
function xiala(i) {
    if (i != null) {
        document.getElementById(i).style.display = "block";
    }
    else {
        document.getElementById("xiala").style.display = "none";
    }

}

function xiala2(i) {
    if (i != null) {
        document.getElementById(i).style.display = "block";
    }
    else {
        document.getElementById("xiala2").style.display = "none";
    }

}
//添加收藏
function AddFavorite(sURL, sTitle) {
    try {
        sURL = window.location.href;
        window.external.addFavorite(sURL, sTitle);
    }
    catch (e) {
        try {
            window.external.addToFavoritesBar(sURL, sTitle, 'slice');
        }
        catch (e) {
            try {
                window.sidebar.addPanel(sTitle, sURL, "");
            }
            catch (e) {
                alert("加入收藏失败，请使用Ctrl+D进行添加");
            }
        }
    }
}

//设为首页
function SetHome(obj, vrl) {
    try {
        vrl = window.location.href;
        obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(vrl);
    }
    catch (e) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            }
            catch (e) {
                alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。");
            }
            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
            prefs.setCharPref('browser.startup.homepage', vrl);
        }
    }
}

//显示时间
function ShowTime(plName) {
    var time = new Date();
    var temp = time.toLocaleString();
    document.getElementById(plName).innerText = temp;
    setTimeout(ShowTime, 60000);
}

//检查单选框是否勾选
function CheckBoxChoose(source, arguments) {
    //检查用户名
    var obj = MM_findObj("cbIsAgree");
    if (obj.checked)
        arguments.IsValid = true;
    else
        arguments.IsValid = false;
}

//检查客户简称长度
function CheckClientNameLen(source, arguments) {
    var pps = arguments.Value.len();
    if (pps > 12)
        arguments.IsValid = false;
    else
        arguments.IsValid = true;
}

//汽车图片切换
function ChangeCarBigPic(disObj, obj) {
    MM_findObj(disObj).src = obj.src;
}

//验证是否上传图片了
function CheckUploadPic(source, arguments) {
    var obj1 = MM_findObj("upload_pic");
    var obj2 = MM_findObj("upload_pic_sel");

    if (obj1.value == "") {
        arguments.IsValid = false;
    }
    else
        arguments.IsValid = true;
}

//隐藏显示条目
function ShowHideFAQ(plName, obj) {
    if (obj.innerHTML.indexOf("+ ") > -1) {
        MM_showHideLayers_pro(plName, '', 'show');
        obj.innerHTML = obj.innerHTML.replace("+ ", "- ");
    }
    else {
        MM_showHideLayers_pro(plName, '', 'hide');
        obj.innerHTML = obj.innerHTML.replace("- ", "+ ");
    }
}