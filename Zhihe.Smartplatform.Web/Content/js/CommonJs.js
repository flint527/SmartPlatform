/**
 * Created by jinglong.fan on 2015/8/6.
 */
//easyui 的提示信息方式  系统中的统一使用方式
//1 表示提示
//2 表示警告
//message({1,"您的操作有错误，请按照正确的方法操作"})
function message(type,text){
    if(type==1){
        $.messager.show({
            title:'提示',
            msg:text+"。",
            timeout:3000,
            showType:'slide'
        });
    }else if(type==2){
        $.messager.show({
            title:'警告',
            msg:text+'！',
            timeout:3000,
            showType:'slide'
        });
    }
}

//系统中拼接url 参数的时候调用的方法   系统中的统一使用方式
//eg: addQueryStringArg("/Home/UserMgr","Id",1);
function addQueryStringArg(url,name,value){
    if(url.indexOf("?")==-1){
        url+="?";
    }else{
        url+="&";
    }
    url+=encodeURIComponent(name)+"="+encodeURIComponent(value);
    return url;
}

