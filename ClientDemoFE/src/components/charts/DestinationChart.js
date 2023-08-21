import React from "react";
import {
  Bar,
  BarChart,
  CartesianGrid,
  Label,
  Legend,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";

const DestinationChart = ({ color, barData, xaxisName, dataKey }) => {
  return (
    <>
      <BarChart
        width={500}
        height={300}
        data={barData}
        margin={{
          top: 30,
          right: 30,
          left: -10,
          bottom: 5,
        }}
      >
        {/* <CartesianGrid strokeDasharray="3 3" /> */}
        <XAxis dataKey={dataKey}>
          <Label value="Destinations" position="insideBottom" />
        </XAxis>
        <YAxis />
        <Tooltip />
        {/* <Legend /> */}
        <Bar dataKey={xaxisName} fill={color} barSize={30} />
      </BarChart>
    </>
  );
};

export default DestinationChart;
