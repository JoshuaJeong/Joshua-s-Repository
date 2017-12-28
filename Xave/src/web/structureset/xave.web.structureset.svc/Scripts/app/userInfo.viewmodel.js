function UserInfoViewModel(app, name, dataModel) {
    var self = this;

    // 데이터
    self.name = ko.observable(name);

    // 작업
    self.logOff = function () {
        dataModel.logout().done(function () {
            app.navigateToLoggedOff();
        }).fail(function () {
            app.errors.push("로그오프하지 못했습니다.");
        });
    };

    self.manage = function () {
        app.navigateToManage();
    };
}
