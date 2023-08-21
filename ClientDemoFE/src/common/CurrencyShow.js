import React from "react";

const CurrencyShow = ({ currencySymbol, currency }) => {
  return (
    <span>
      {currencySymbol
        ? `${currencySymbol} ${currency}`
        : parseInt(currency).toLocaleString("en-IN", {
            maximumFractionDigits: 0,
            style: "currency",
            currency: "INR",
          })}
    </span>
  );
};

export default CurrencyShow;
