using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Extensions
{
    /// <summary>
    /// 错误信息对应表
    /// </summary>
    public enum ErrorInfo
    {
        /// <summary>
        /// 请求成功（1）
        /// </summary>
        OK = 1,
        /// <summary>
        /// 新增修改删除失败
        /// </summary>
        UpError = 2,
        /// <summary>
        /// 没有操作权限 （3）
        /// </summary>
        RoleError = 3,
        /// <summary>
        /// 余额不足 （4）
        /// </summary>
        AmountError = 4,
        /// <summary>
        /// 超时
        /// </summary>
        TimeOut=5,
        /// <summary>
        /// 没有设备码
        /// </summary>
        ImeiError=6,
        /// <summary>
        /// 密码锁
        /// </summary>
        PwdLock=7,
        /// <summary>
        ///  token验证失败(8)
        /// </summary>
        TokenError = 8,
        /// <summary>
        ///  签名不匹配(9)
        /// </summary>
        SignError = 9,
        /// <summary>
        /// 帐号或密码错误（10）
        /// </summary>
        PassError = 10,
        /// <summary>
        /// 帐号锁定（11）
        /// </summary>
        Lock = 11,
        /// <summary>
        /// 忘记密码 帐号不存在（12）
        /// </summary>
        UserNone = 12,
        /// <summary>
        /// 头像上传失败（13）
        /// </summary>
        UploadFailure = 13,
        /// <summary>
        /// 短信黑名单（14）
        /// </summary>
        SMSBlackError = 14,
        /// <summary>
        /// 短信验证码错误（15）
        /// </summary>
        SMSVerifyError = 15,
        /// <summary>
        /// 服务器内部错误（101）
        /// </summary>
        ServerError = 101,
        /// <summary>
        /// 参数无效
        /// </summary>
        ParameterError = 102,
    }
}