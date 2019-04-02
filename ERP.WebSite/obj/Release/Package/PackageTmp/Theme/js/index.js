$(function() {
    /**
     * 全局变量
     */
    //接口地址
    var dataUrl = '';

    /**
     * 定义方法
     */
    
    //提示信息处理


    /**
     * 定义事件
     */
    //点击发送验证码按钮
   

    // 下一步 验证功能

    $('.user').blur(function() {
        $('.alert-user').html('');
    })
    $('.pwd').blur(function() {
        $('.alert-pwd').html('');
    })
    $('.code').blur(function() {
        $('.alert-code').html('');
    })

    //登录按钮
    $('.btn-login').click(function(){
        $('.alert-user').html('');
        $('.alert-pwd').html('');
        $('.alert-code').html('');
        if($('.user').val().replace(/\s/g,'') ==''){
            $('.alert-user').html('请输入用户名');
            return
        }
        if($('.pwd').val().replace(/\s/g,'') ==''){
            $('.alert-pwd').html('请输入密码');
            return
        }
        if($('.code').val().replace(/\s/g,'') ==''){
            $('.alert-code').html('请输入验证码');
            return
        }
        console.log('登录')
        // $('.alert-login').html('用户名或密码错误')
    })
    //重置按钮
    $('.btn-reset').click(function(){
        $('.user').val('');
        $('.pwd').val('');
        $('.code').val('');
        $('.alert-user').html('');
        $('.alert-pwd').html('');
        $('.alert-code').html('');
    })


    /**
     *  定义接口
     */
    // 发送验证码 
    function getMSM(num, cb) {
        var url = dataUrl + "/getMSM";
        var dataStr = '{"userkey": "' + userkey + '","userId":"' + userid + '","phoneN":"' + num + '"}';
        $.ajax({
            type: "POST",
            url: url,
            async: false,
            cache: true,
            data: dataStr,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(msg) {
                var obj = msg.d;
                localStorage.setItem('Obj', obj); //缓存数据
                var jsonObj = $.parseJSON(obj)[0]; //转化为Json对象
                var msgcode = jsonObj.msgcode;
                var dataMsg = jsonObj.msg;
                if (msgcode != "" && msgcode != null && msgcode != undefined) {
                    console.log(dataMsg);
                } else {
                    cb && cb(jsonObj)
                }
            },
            error: function(msg) {
                console.log('error');
            }
        });
    }

})
