import { url } from "../common/const";
import { get, post } from "../common/https";
/**This api for get city list*/
export const GetCityList = () => {
  const getdata = get(`${url}/api/City/list`);
  return getdata;
};
/**This service for add city list */
export const AddCityList = (data) => post(`${url}/api/City/Add`, data);

/**This service for update city list */
export const UpdateCityList = (data) => post(`${url}/api/City/Update`, data);
