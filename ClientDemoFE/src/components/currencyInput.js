export const currencyMask = (e) => {
  let val = e.target.value;
  val = val.replace(/\D/g, "");
  val = val.replace(/,/g, "");
  if (val.length > 3) {
    let noCommas = Math.ceil(val.length / 3) - 1;
    let remain = val.length - noCommas * 3;
    let newVal = [];
    for (let i = 0; i < noCommas; i++) {
      newVal.unshift(val.substr(val.length - i * 3 - 3, 3));
    }
    newVal.unshift(val.substr(0, remain));
    e.target.value = newVal;
  } else {
    e.target.value = val;
  }
  return e;

  // if (typeof amount != "undefined") {
  //   var x = amount.toString();
  //   var afterPoint = "";
  //   if (x.indexOf(".") > 0)
  //     afterPoint = x.substring(x.indexOf("."), x.length);
  //   x = Math.floor(x);
  //   x = x.toString();
  //   var lastThree = x.substring(x.length - 3);
  //   var otherNumbers = x.substring(0, x.length - 3);
  //   if (otherNumbers != "") lastThree = "," + lastThree;
  //   var res =
  //     otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ",") +
  //     lastThree +
  //     afterPoint;
  //   return "₹" + res;
  // }

  return "";
};
