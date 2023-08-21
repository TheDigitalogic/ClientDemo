import { url } from "../common/const";
import { get, post } from "../common/https";
/**This api for get City SiteSeeing  list*/
export const getCitySiteSeeingList = () => {
  const getdata = get(`${url}/api/CitySiteSeeing/list`);
  return getdata;
};
/**This api for save City SiteSeeing list*/
export const saveCitySiteSeeingList = (data) =>
  post(`${url}/api/CitySiteSeeing/save`, data);
