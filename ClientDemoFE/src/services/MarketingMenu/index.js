import { url } from "../common/const";
import { get, post, post_common, get_common } from "../common/https";

export const uploadXlsxfiles = (data) =>
  post_common(`${url}/api/Travelling_company/PreviewImportFile`, data);

/**This service for fetching Data from travelling company */
export const getTravellingCompany = (params) => {
  // const getdata = get(`${url}/api/Travelling_company/list?` + params);
  const getdata = get(`${url}/api/Travelling_company/list?` + params);
  return getdata;
};
/**This service for update/save travelling company */
/**This service for download template */
export const saveTravellingCompany = (data) =>
  post(`${url}/api/Travelling_company/SaveTravellingCompany`, data);

export const downloadTemplate = (params) => {
  const getdata = get(
    `${url}/api/Travelling_company/downloadTemplate?` + params
  );
  return getdata;
};
/**This service for sent mail */
export const sentEmailService = (data) =>
  post(`${url}/api/Travelling_company/sendMail`, data);
