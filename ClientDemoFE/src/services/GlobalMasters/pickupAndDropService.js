import { url } from "../common/const";
import { get, post } from "../common/https";
/**This api for get PickupAndDrop list*/
export const GetPickupDropList = () => {
  const getdata = get(`${url}/api/DestinationPickupAndDrop/list`);
  return getdata;
};
/**This api for save PickupAndDrop list*/
export const SavePickupDropList = (data) =>
  post(`${url}/api/DestinationPickupAndDrop/save`, data);
