import { url } from "../common/const";
import { get } from "../common/https";

/**This service for get destination list*/
export const GetProcessLogsList = (params) => {
  const getdata = get(`${url}/api/ProcessLogs/list?` + params);
  return getdata;
};
