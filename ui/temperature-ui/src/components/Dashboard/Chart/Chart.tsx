import React from 'react';
import { Line } from 'react-chartjs-2';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { data } from 'react-router-dom';

// Chart.js modüllerini kaydet
ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

interface ChartProps
{
    xLabels? : Array<string>,
    datas?: Array<any>,
    chartLabel?: string,
    color?: string
}

const ChartComponent: React.FC<ChartProps> = ({xLabels, datas, chartLabel, color}) => {
  const data = {
    labels: xLabels,
    datasets: [
      {
        label: chartLabel,
        data: datas,
        borderColor: color == 'blue' ? '#3498db' : '#e74c3c',
        borderWidth: 2,
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top' as const,
      },
      title: {
        display: true,
        text: `${chartLabel} Chart`
      },
    },
  };

  return <Line data={data} options={options} />;
};

export default ChartComponent;