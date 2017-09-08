export class CompanyRegister {
    
    constructor(
        public name1 = '',
        public dateCommencement = '',
        public companyType = 'Pty',// Propreitory or Public
        public liability = 'Shares',//Shares or Gaurantee
        public coyBeTrustee = 'Yes', //Yes or No
        public dirLn = '',
        public dirFn = '',
        public dirTfn = '',
        public dirDob = '',
        public dirPob = '',
        public dirAddr = '',
        public dirPh = '',
        public dirEmail = '',
        public dirDrLicense = '',
        public sharesNo = '',
        public shareHoldName = '',
        public shareHoldAddr = '',
        public officeAddr = '',
        public operatingAddr = '',
        public businessNature = '',
        public occupation = '',
        public position = 'Director',// Director, Secretary, Public Officer
        public printing = 'No', // Yes, No
        public gstReg = 'Yes', // Yes, No
        public employeeOthers = 'Yes', // Yes, No
        public employeeOthersNo = '',
        public payRoyalty = 'Yes', // Yes, No
        public isImporting = 'Yes', // Yes, No
        public fuelTaxCredit = 'Yes', // Yes, No
                                              
    ){}

}


/*     constructor(public firstName = '',
        public lastName = '',
        public email = '',
        public sendCatalog = false,
        public addressType = 'home',
        public street1?: string,
        public street2?: string,
        public city?: string,
        public state = '',
        public zip?: string) { } */