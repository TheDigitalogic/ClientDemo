import { url } from "../common/const";
import { get, post } from "../common/https";
/**This api for get dashoard list*/
export const GetDashboardChartData = (params) => {
  const getdata = get(`${url}/api/Dashboard/list?` + params);
  return getdata;
};
