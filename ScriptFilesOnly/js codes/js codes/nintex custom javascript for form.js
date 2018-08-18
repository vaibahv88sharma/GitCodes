// fnSupplyItemsIsEmpty is called from :- form control's > Validation > Custom validation function :- fnSupplyItemsIsEmpty

function fnSupplyItemsIsEmpty(source, arguments){ 
    console.log('logging nintex form'); 
    console.log(source); 
    console.log(arguments); 
    // debugger; 

    // varArgumentID should  come from 'source' present in function paramater

    var obj = NWF$('#'+varArgumentID) 
    // id = ListForm_formFiller_FormView_ctl66_1c98b6aa_ce33_4403_a3df_031698f71c17 
    // name = ListForm$formFiller$FormView$ctl66$1c98b6aa_ce33_4403_a3df_031698f71c17 
    console.log(obj); 
}
