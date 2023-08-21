import * as XLSX from "xlsx";
export const downloadExcel = (exportedData, name) => {
  const worksheet = XLSX.utils.json_to_sheet(exportedData);
  const workbook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(workbook, worksheet, "Sheet1");
  XLSX.writeFile(workbook, `${name}.xlsx`);
};

/**This is function for unique destination filter */
export const filterUniqueDestination = (json) => {
  const uniqueObjectsSet = new Set();
  const unique = json.filter((item) => {
    const isDuplicate = uniqueObjectsSet.has(item.destination_id);
    uniqueObjectsSet.add(item.destination_id);
    return !isDuplicate;
  });
  return unique;
};

/**This is function for unique city filter */
export const filterUniqueCity = (json) => {
  const uniqueObjectsSet = new Set();
  const unique = json.filter((item) => {
    const isDuplicate = uniqueObjectsSet.has(item.city_id);
    uniqueObjectsSet.add(item.city_id);
    return !isDuplicate;
  });
  return unique;
};
/**This is function for unique hotel filter */
export const filterUniqueHotel = (json) => {
  const uniqueObjectsSet = new Set();
  const unique = json.filter((item) => {
    const isDuplicate = uniqueObjectsSet.has(item.hotel_id);
    uniqueObjectsSet.add(item.hotel_id);
    return !isDuplicate;
  });
  return unique;
};
/**This is function for unique transport filter */
export const filterUniqueTransport = (json) => {
  const uniqueObjectsSet = new Set();
  const unique = json.filter((item) => {
    const isDuplicate = uniqueObjectsSet.has(item.transport_rate_id);
    uniqueObjectsSet.add(item.transport_rate_id);
    return !isDuplicate;
  });
  return unique;
};
