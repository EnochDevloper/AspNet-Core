/*
            FileReader共有4种读取方法：
            1.readAsArrayBuffer(file)：将文件读取为ArrayBuffer。
            2.readAsBinaryString(file)：将文件读取为二进制字符串
            3.readAsDataURL(file)：将文件读取为Data URL
            4.readAsText(file, [encoding])：将文件读取为文本，encoding缺省值为'UTF-8'
                         */
var wb;//读取完成的数据
var rABS = false; //是否将文件读取为二进制字符串

//导入
function GetExcelData(files, columns, sheetName) {

    var result = {};
    var errMsg = "";
    if (!files) {
        return;
    }
    const IMPORTFILE_MAXSIZE = 10 * 1024;//这里可以自定义控制导入文件大小
    var suffix = files.name.split(".")[1]
    if (suffix != 'xls' && suffix != 'xlsx') {
        errMsg = '导入的文件格式不正确!';
    }

    if (files.size / 1024 > IMPORTFILE_MAXSIZE) {
        errMsg = '导入的表格文件不能大于10M';
    }


    var f = files;
    var reader = new FileReader();
    reader.onload = function (e) {

        var data = e.target.result;
        if (rABS) {
            wb = XLSX.read(btoa(fixdata(data)), {//手动转化
                type: 'base64'
            });
        } else {
            wb = XLSX.read(data, {
                type: 'binary'
            });
        }

        //wb.SheetNames[0]是获取Sheets中第一个Sheet的名字
        //wb.Sheets[Sheet名]获取第一个Sheet的数据


        //默认是第一个Sheet
        var excelData = XLSX.utils.sheet_to_json(wb.Sheets[wb.SheetNames[0]]);
        //自己决定Sheet
        if (sheetName) {
            excelData = XLSX.utils.sheet_to_json(wb.Sheets[sheetName]);
        }

        //判读excel是否有数据
        if (excelData.length == 0) {
            errMsg = '请选择有数据的表导入数据';
        }

        //标题数组
        if (columns.length == 0) {
            errMsg = '请创建Excel规则数组(标题行)';
        }

        var isRight = true;     //列名是否正常
        var LackCol = "";       //缺少的列或者命名错误的列

        if (excelData[0]) {
            for (var i = 0; i < columns.length; i++) {

                var col = columns[i];
                var lie = excelData[0][col];
                if (lie == undefined) {
                    isRight = false;
                    LackCol = col;
                    break;
                }
            }
        }
        if (!isRight) {
            errMsg = "请导入正确的Excel模板 异常列'" + LackCol + "'";
        }

        result = { msg: errMsg, data: excelData };
        importData(result);
    };

    if (rABS) {
        reader.readAsArrayBuffer(f);
    } else {
        reader.readAsBinaryString(f);
    }
}

function fixdata(data) { //文件流转BinaryString
    var o = "",
        l = 0,
        w = 10240;
    for (; l < data.byteLength / w; ++l) o += String.fromCharCode.apply(null, new Uint8Array(data.slice(l * w, l * w + w)));
    o += String.fromCharCode.apply(null, new Uint8Array(data.slice(l * w)));
    return o;
}