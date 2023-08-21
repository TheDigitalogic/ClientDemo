import { toast } from "react-toastify";
/**This is fucntion for error notification*/
export const errornotify = (status) =>
  toast(status, {
    position: "bottom-center",
    hideProgressBar: true,
    className: "bg-danger text-white",
  });
/**This is fucntion for success notification*/
export const successNotify = (status) =>
  toast(status, {
    position: "bottom-center",
    hideProgressBar: false,
    className: "bg-success text-white",
  });
