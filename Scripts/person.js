
//currently not in use for later use
function getAjax(url) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url,
            success: data => {
                resolve(data);
            },
            error: err => {
                reject(err);
            }
        })
    });
} 

function postAjax(url,dataOject) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url,
            type: "POST",
            data: dataOject,
            success: data => {
                resolve(data);
            },
            error: err => {
                reject(err);
            }
        })
    });
} 

$(function () {
    $(".deletePerson").on("click", async function () {
        const url = "/persons/DeleteConfirmedAajax";
        if (confirm("delete ?")) {
            try {
               
                const data = await postAjax(url, { id: this.id });
                if (data.status === "fail") {
                    alert('cant delete person');
                    return;
                }
                $(this).closest("tr").remove();

            }
            catch (err) {
                alert(err.message)
            }
        }
    });
});