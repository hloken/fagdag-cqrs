angular.module('fagdagCqrsHotel', [
        'ngRoute',
        'ngResource' /*,
    'ngCookies',
    'pascalprecht.translate',
    'ui.bootstrap',
    'ngSanitize',
    'ui.validate'*/
    ])
    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.
            when('/rooms', { controller: 'RoomsController', templateUrl: 'App/Booking/Rooms.html' }).
            otherwise({ redirectTo: '/rooms' });
    }]);

//when('tenantinitialization', '/tenant/initialization', { controller: "TenantInitializationController", templateUrl: 'App/TenantInitialization/tenantinitialization.html' });
        //when('auditlog', '/auditlog', { controller: "AuditLogController", templateUrl: 'App/AuditLog/AuditLog.html' });
        //when('paycodes', '/:companyId/paycodes', { controller: "PaycodesController", templateUrl: 'App/Paycodes/paycodes.html' });
        //when('paycode.new', '/:companyId/paycodes/new', { controller: "EditPaycodeController", templateUrl: 'App/Paycodes/editpaycode.html' });
        //when('paycode.edit', '/:companyId/paycodes/:paycodeId', { controller: "EditPaycodeController", templateUrl: 'App/Paycodes/editpaycode.html' });
        //when('employees', '/company/:companyId/employees', { controller: "EmployeesController", templateUrl: 'App/Employee/employees.html' });
        //when('employee.edit', '/company/:companyId/employees/edit/:employeeId', { controller: "EditEmployeeController", templateUrl: 'App/Employee/EditEmployee.html' });
        //when('employee.new', '/company/:companyId/employees/new', { controller: "CreateEmployeeController", templateUrl: 'App/Employee/EditEmployee.html' });
        //when('transactions.register', '/company/:companyId/employee/:employeeId/transactions/salaryintervals/:salaryIntervalId', { controller: "TransactionsController", templateUrl: 'App/Transactions/transactions.html' });
        //when('transactions.selectsalaryinterval', '/company/:companyId/employee/:employeeId/transactions/salaryintervals', { controller: "TransactionsController", templateUrl: 'App/Transactions/transactions.html' });
        //when('payslip', '/company/:companyId/payslips/employee/:employeeId/salaryintervals/:salaryIntervalId', { controller: "PayslipController", templateUrl: 'App/Payslip/payslip.html' });
        //when('payslips', '/company/:companyId/payslips/employee/:employeeId/salaryintervals', { controller: "PayslipController", templateUrl: 'App/Payslip/payslip.html' });
        //when('employment.edit', '/:companyId/people/:employeeId/employments/edit/:employmentId', { controller: "EditEmploymentController", templateUrl: 'App/Employment/EditEmployment.html' });
        //when('employment.new', '/:companyId/people/:employeeId/employments/new', { controller: "CreateEmploymentController", templateUrl: 'App/Employment/CreateEmployment.html' });
        //when('chartofaccounts', '/:companyId/chartofaccounts', { controller: "ChartOfAccountsController", templateUrl: 'App/BookkeepingAccounts/chartofaccounts.html' });
        //when('chartofaccounts.new', '/:companyId/chartofaccounts/new', { controller: "CreateBookkeepingAccountController", templateUrl: 'App/BookkeepingAccounts/editbookkeepingaccount.html' });
        //when('chartofaccounts.edit', '/:companyId/chartofaccounts/edit/:accountId', { controller: "EditBookkeepingAccountController", templateUrl: 'App/BookkeepingAccounts/editbookkeepingaccount.html' });
        //when('unions', '/company/:companyId/unions', { controller: 'UnionsController', templateUrl: 'App/Unions/Unions.html' });
        //when('union.new', '/company/:companyId/unions/new', { controller: 'CreateUnionController', templateUrl: 'App/Unions/EditUnion.html' });
        //when('union.edit', '/company/:companyId/unions/edit/:unionId', { controller: 'EditUnionController', templateUrl: 'App/Unions/EditUnion.html' });
        //when('additions', '/company/:companyId/additions', { controller: 'AdditionsController', templateUrl: 'App/Additions/additions.html' });
        //when('addition.new', '/company/:companyId/additions/new', { controller: 'NewAdditionController', templateUrl: 'App/Additions/newAddition.html' });
        //when('addition.edit', '/company/:companyId/additions/edit/:additionId', { controller: 'EditAdditionController', templateUrl: 'App/Additions/editAddition.html' });
        //when('company.edit', '/company/edit/:companyId', { controller: "CompanyController", templateUrl: 'App/Company/company.html' });
        //when('company.new', '/company/new', { controller: "CreateCompanyController", templateUrl: 'App/Company/createcompany.html' });
        //when('company.banking', '/company/:companyId/banking', { controller: "CompanyBankingController", templateUrl: 'App/Banking/companyBanking.html' });
        //when('company.bookkeeping', '/company/:companyId/bookkeeping', { controller: "CompanyBookkeepingAccountsController", templateUrl: 'App/BookkeepingAccounts/companyBookkeepingAccounts.html' });
        //when('company.salaryintervals', '/company/:companyId/salaryintervals', { controller: "SalaryIntervalsController", templateUrl: 'App/SalaryIntervals/salaryintervals.html' });
        //when('company.salaryintervals.current', '/company/:companyId/salaryintervals/current', { controller: "CurrentSalaryIntervalController", templateUrl: 'App/SalaryIntervals/current.html' });
        //when('company.salaryintervals.close', '/company/:companyId/salaryintervals/close', { controller: "CloseController", templateUrl: 'App/SalaryIntervals/close.html' });
        //when('company.salaryintervals.approve', '/company/:companyId/salaryintervals/approve', { controller: "ApproveController", templateUrl: 'App/SalaryIntervals/approve.html' });
        //when('costcenters', '/company/:companyId/costcenters', { controller: "CostCentersController", templateUrl: "App/CostCenter/costcenters.html" });
        //when('businesslocation', '/company/:companyId/businesslocation', { controller: "BusinessLocationController", templateUrl: 'App/BusinessLocation/businesslocation.html' });
        //when('businesslocation.new', '/company/:companyId/businesslocation/new', { controller: "CreateBusinessLocationController", templateUrl: 'App/BusinessLocation/createbusinesslocation.html' });
        //when('businesslocation.edit', '/company/:companyId/businesslocation/:businessLocationId', { controller: "EditBusinessLocationController", templateUrl: 'App/BusinessLocation/EditBusinessLocation.html' });
        //when('reports.bookkeeping', '/:companyId/reports/bookkeeping', { controller: "BookkeepingReportController", templateUrl: 'App/Reports/bookkeepingreport.html' });
        //when('reports.bookkeepingforsalaryinterval', '/:companyId/reports/bookkeeping/:salaryIntervalId', { controller: "BookkeepingReportController", templateUrl: 'App/Reports/bookkeepingreport.html' });
        //when('reports.bankinglistforsalaryinterval', '/:companyId/reports/bankinglists/:salaryIntervalId', { controller: "BankingListController", templateUrl: 'App/Reports/bankinglist.html' });
        //when('reports.bankinglists', '/:companyId/reports/bankinglists', { controller: "BankingListController", templateUrl: 'App/Reports/bankinglist.html' });
        //when('taxcard', '/:companyId/taxcard', { controller: "TaxCardController", templateUrl: 'App/TaxCard/tax-card.html' });
        //when('taxcard.details', '/:companyId/taxcard/:jobId/details', { controller: "TaxCardController", templateUrl: 'App/TaxCard/tax-card.html' });
        //when('taxcard.overview', '/:companyId/taxcard/overview', { controller: "TaxCardController", templateUrl: 'App/TaxCard/tax-card-overview.html' });
        //when('taxcard.login', '/:companyId/taxcard/:jobId/login', { controller: "TaxCardController", templateUrl: 'App/TaxCard/tax-card-login.html' });
        //when('taxcard.pin', '/:companyId/taxcard/:jobId/pin', { controller: "TaxCardController", templateUrl: 'App/TaxCard/tax-card-pin.html' });
        //when('taxcard.progress', '/:companyId/taxcard/:jobId/progress', { controller: "TaxCardController", templateUrl: 'App/TaxCard/tax-card-progress.html' });
        //when('taxcard.success', '/:companyId/taxcard/:jobId/success', { controller: "TaxCardController", templateUrl: 'App/TaxCard/tax-card-success.html' });
        //when('taxcard.failed', '/:companyId/taxcard/:jobId/failed', { controller: "TaxCardController", templateUrl: 'App/TaxCard/tax-card-failed.html' });
        //when('taxcard.cancelled', '/:companyId/taxcard/:jobId/cancelled', { controller: "TaxCardController", templateUrl: 'App/TaxCard/tax-card-cancelled.html' });
        //when('taxcard.altinnconfiguration', '/tax/company/:companyId/altinnconfiguration', { controller: "AltinnConfigurationController", templateUrl: 'App/TaxCard/altinnconfiguration.html' });
        //when('designguide', '/designguide', { controller: "DesignGuideController", templateUrl: 'App/DesignGuide/design-guide.html' });
        //when('designguide.modal', '/designguide/modal', { controller: "DesignGuideController", templateUrl: 'App/DesignGuide/design-guide-modal.html' });
        //when('metadata', '/metadata', { controller: "MetaDataController", templateUrl: 'App/MetaData/metadata.html' });

        /* Prototypes */
        //when('prototype.employeetransaction', '/prototype/employee/transaction/mars', { controller: "PrototypeController", templateUrl: 'App/Prototype/mars.html' });
        //when('prototype.employeetransaction', '/prototype/employee/transaction/april', { controller: "PrototypeController", templateUrl: 'App/Prototype/april.html' });


    //}
    //]);
    
