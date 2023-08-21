import { url } from "../common/const";
import { get, post } from "../common/https";
/**This api for get hotel list*/
export const GetHotelList = () => {
  const getdata = get(`${url}/api/Hotel/list`);
  return getdata;
};
/**This api for save hotel list*/
export const SaveHotelList = (data) => post(`${url}/api/Hotel/save`, data);
/**This service for add hotel list */
export const AddHotelList = (data) => post(`${url}/api/Hotel/Add`, data);

/**This service for update hotel list */
export const UpdateHotelList = (data) => post(`${url}/api/Hotel/Update`, data);
