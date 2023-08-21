import { url } from "../common/const";
import { get, post } from "../common/https";
/**This api for get Hotel Room list*/
export const GetTransportRateList = () => {
  const getdata = get(`${url}/api/TransportRate/list`);
  return getdata;
};
/**This api for save Hotel Room list*/
export const SaveTransportRateList = (data) =>
  post(`${url}/api/TransportRate/save`, data);
