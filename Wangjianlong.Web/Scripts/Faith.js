$(function () {
    $("a[name='Delete']").click(function () {
        if (confirm("您确定要删除吗?一旦删除无法恢复找回数据")) {
            var node = $(this).parent().parent();
            ShowMessage("正在删除,请稍等......");
            var href = $(this).attr("href");
            $.request(href, null, function (json) {
                if (json.result == 0) {
                    ShowErrorMessage(json.content);
                } else {
                    ShowSuccessMessage("成功删除");
                    node.remove();
                }
            });
        }

        return false;
    });



});