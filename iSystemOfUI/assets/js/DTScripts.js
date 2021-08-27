
// load danh sách option vào <select> có id chỉ định
function loadOptions(elementId, data, CLValue, CLName, baseRow, defaultValue) {
    $(`#${elementId} option`).remove()
    if (baseRow) {
        $(`#${elementId}`).append(`<option value="">Chọn</option>`)
    }
    data.forEach(item => {
        let html = ""
        if (item.Name == defaultValue)
            html = `<option selected value="${item[CLValue]}">${item[CLName]}</option>`
        else
            html = `<option value="${item[CLValue]}">${item[CLName]}</option>`
        $(`#${elementId}`).append(html)
    })
}