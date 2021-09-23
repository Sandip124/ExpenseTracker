
let notyf = new Notyf({
    duration: 500000,
    dismissible: true,
    position: {
        x: 'center',
        y: 'bottom',
    },
})

function notifySuccess(notificationObj) {
    if (typeof (notificationObj) != "object") {
        notificationObj = {message: notificationObj}
    }
    notyf.success({
            message: notificationObj.message || "",
        })
}

function notifyError(notificationObj) {
    if (typeof (notificationObj) != "object") {
        notificationObj = {message: notificationObj}
    }
    notyf.error({
        message: notificationObj.message || "",
    })
}

function notifyInfo(notificationObj) {
    if (typeof (notificationObj) != "object") {
        notificationObj = {message: notificationObj}
    }
    notyf.open({
        message: notificationObj.message || "",
        types: [
            {
                type: 'info',
                background: 'blue',
                icon: false
            }
        ]
    })
}