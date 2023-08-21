import { url } from "../common/const";
import { get, post } from "../common/https";
/**This api for get Transport list*/
export const GetVehicleList = () => {
  const getdata = get(`${url}/api/Transport/list`);
  return getdata;
};
/**This api for save Transport list*/
export const SaveVehicleList = (data) =>
  post(`${url}/api/Transport/save`, data);
/**This service for add Transport list */
export const AddVehicleList = (data) => post(`${url}/api/Transport/Add`, data);

/**This service for update Transport list */
export const UpdateVehicleList = (data) =>
  post(`${url}/api/Transport/Update`, data);
