using System;
using System.Drawing;
using System.Windows.Forms;
using TrainCrew;

class DistancePerTime: Form
{
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new DistancePerTime());
	}

	private const int fontSize = 16, gridSize = 22;

	private static T CreateControl<T>(Control parent, float x, float y, float width, float height)
	where T: Control, new()
	{
		T control = new T();
		control.Location = new Point((int)(gridSize * x), (int)(gridSize * y));
		control.Size = new Size((int)(gridSize * width), (int)(gridSize * height));
		if (parent != null) parent.Controls.Add(control);
		return control;
	}

	private Panel mainPanel;

	private Label distanceTitleLabel, distanceLabel;
	private Label timeTitleLabel, timeLabel;
	private Label distancePerTimeTitleLabel, distancePerTimeLabel;
	private Label kmPerHourTitleLabel, kmPerHourLabel;
	private Label currentSpeedTitleLabel, currentSpeedLabel;
	private Label timingPredictionTitleLabel, timingPredictionLabel;

	public DistancePerTime()
	{
		float panelWidth = 11;
		string fontName = "MS UI Gothic";
		Font normalFont = new Font(fontName, fontSize, GraphicsUnit.Pixel);
		Font doubleFont = new Font(fontName, fontSize * 2, GraphicsUnit.Pixel);
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
		this.Text = "距離÷時間";
		this.MaximizeBox = false;
		this.ClientSize = new Size((int)(gridSize * (panelWidth + 1)), gridSize * 19);
		SuspendLayout();

		mainPanel = CreateControl<Panel>(this, 0.5f, 0.5f,  panelWidth, 18);

		distanceTitleLabel = CreateControl<Label>(mainPanel, 0, 0, panelWidth, 1);
		distanceTitleLabel.Font = normalFont;
		distanceTitleLabel.Text = "次のポイントまでの距離は";
		distanceTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
		distanceLabel = CreateControl<Label>(mainPanel, 0, 1, panelWidth, 2);
		distanceLabel.Font = doubleFont;
		distanceLabel.Text = "#### m";
		distanceLabel.TextAlign = ContentAlignment.MiddleRight;

		timeTitleLabel = CreateControl<Label>(mainPanel, 0, 3, panelWidth, 1);
		timeTitleLabel.Font = normalFont;
		timeTitleLabel.Text = "次のポイントまでの時間は";
		timeTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
		timeLabel = CreateControl<Label>(mainPanel, 0, 4, panelWidth, 2);
		timeLabel.Font = doubleFont;
		timeLabel.Text = "#### 秒";
		timeLabel.TextAlign = ContentAlignment.MiddleRight;

		distancePerTimeTitleLabel = CreateControl<Label>(mainPanel, 0, 6, panelWidth, 1);
		distancePerTimeTitleLabel.Font = normalFont;
		distancePerTimeTitleLabel.Text = "この距離をこの時間で割ると";
		distancePerTimeTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
		distancePerTimeLabel = CreateControl<Label>(mainPanel, 0, 7, panelWidth, 2);
		distancePerTimeLabel.Font = doubleFont;
		distancePerTimeLabel.Text = "##.# m/s";
		distancePerTimeLabel.TextAlign = ContentAlignment.MiddleRight;

		kmPerHourTitleLabel = CreateControl<Label>(mainPanel, 0, 9, panelWidth, 1);
		kmPerHourTitleLabel.Font = normalFont;
		kmPerHourTitleLabel.Text = "すなわち";
		kmPerHourTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
		kmPerHourLabel = CreateControl<Label>(mainPanel, 0, 10, panelWidth, 2);
		kmPerHourLabel.Font = doubleFont;
		kmPerHourLabel.Text = "###.# km/h";
		kmPerHourLabel.TextAlign = ContentAlignment.MiddleRight;

		currentSpeedTitleLabel = CreateControl<Label>(mainPanel, 0, 12, panelWidth, 1);
		currentSpeedTitleLabel.Font = normalFont;
		currentSpeedTitleLabel.Text = "一方、現在の速度は";
		currentSpeedTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
		currentSpeedLabel = CreateControl<Label>(mainPanel, 0, 13, panelWidth, 2);
		currentSpeedLabel.Font = doubleFont;
		currentSpeedLabel.Text = "###.# km/h";
		currentSpeedLabel.TextAlign = ContentAlignment.MiddleRight;

		timingPredictionTitleLabel = CreateControl<Label>(mainPanel, 0, 15, panelWidth, 1);
		timingPredictionTitleLabel.Font = normalFont;
		timingPredictionTitleLabel.Text = "この速度で次のポイントに着くのは";
		timingPredictionTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
		timingPredictionLabel = CreateControl<Label>(mainPanel, 0, 16, panelWidth, 2);
		timingPredictionLabel.Font = doubleFont;
		timingPredictionLabel.Text = "### 秒延";
		timingPredictionLabel.TextAlign = ContentAlignment.MiddleRight;

		ResumeLayout();

		Load += LoadHandler;
		FormClosed += FormClosedHandler;
	}

	private Timer timer = null;
	private bool trainCrewEnabled = false;
	private int stationDataRequestPrevent = 0;

	private void LoadHandler(object sender, EventArgs e)
	{
		TrainCrewInput.Init();
		TrainCrewInput.RequestStaData();
		trainCrewEnabled = true;
		timer = new Timer();
		timer.Interval = 15;
		timer.Tick += TickHandler;
		timer.Start();
	}

	private void FormClosedHandler(object sender, EventArgs e)
	{
		trainCrewEnabled = false;
		TrainCrewInput.Dispose();
		if (timer != null) timer.Stop();
	}

	private void TickHandler(object sender, EventArgs e)
	{
		if (!trainCrewEnabled) return;
		TrainState trainState = TrainCrewInput.GetTrainState();
		if (stationDataRequestPrevent <= 0)
		{
			if (trainState.stationList.Count == 0)
			{
				TrainCrewInput.RequestStaData();
				stationDataRequestPrevent = 10;
			}
		}
		else
		{
			stationDataRequestPrevent--;
		}
		distanceLabel.Text = string.Format("{0} m", Math.Truncate(trainState.nextUIDistance));
		currentSpeedLabel.Text = string.Format("{0:0.0} km/h", trainState.Speed);
		float distance = trainState.nextUIDistance;
		TimeSpan? timeToArrive = null;
		if (trainState.nowStaIndex < trainState.stationList.Count)
		{
			int minIndex = trainState.nowStaIndex;
			float minScore = Math.Abs(trainState.nextUIDistance - trainState.nextStaDistance);
			for (int i = trainState.nowStaIndex + 1; i < trainState.stationList.Count; i++)
			{
				float thisStaDistance =
					trainState.nextStaDistance +
					trainState.stationList[i].TotalLength -
					trainState.stationList[trainState.nowStaIndex].TotalLength;
				float score = Math.Abs(trainState.nextUIDistance - thisStaDistance);
				if (score < minScore)
				{
					minIndex = i;
					minScore = score;
				}
			}
			timeToArrive = trainState.stationList[minIndex].ArvTime;
		}
		if (timeToArrive.HasValue)
		{
			double time = timeToArrive.Value.TotalSeconds - trainState.NowTime.TotalSeconds;
			timeLabel.Text = string.Format("{0} 秒", Math.Ceiling(time));
			double distancePerTime = time <= 0 ? Double.PositiveInfinity : distance / time;
			if (distancePerTime >= 1000)
			{
				distancePerTimeLabel.Text = "∞ m/s";
				kmPerHourLabel.Text = "∞ km/h";
			}
			else
			{
				distancePerTimeLabel.Text = string.Format("{0:0.0} m/s", distancePerTime);
				kmPerHourLabel.Text = string.Format("{0:0.0} km/h", distancePerTime * 3.6);
			}
			double estimatedTimeToArrive = trainState.Speed <= 0 ? Double.PositiveInfinity : distance / (trainState.Speed / 3.6);
			double estimatedTimeDifference = trainState.NowTime.TotalSeconds + estimatedTimeToArrive - timeToArrive.Value.TotalSeconds;
			double etdSeconds = Math.Floor(estimatedTimeDifference);
			if (etdSeconds >= 1000)
			{
				timingPredictionLabel.Text = "∞ 秒延";
			}
			else if (etdSeconds >= 1)
			{
				timingPredictionLabel.Text = string.Format("{0} 秒延", etdSeconds);
			}
			else if (etdSeconds <= -1)
			{
				timingPredictionLabel.Text = string.Format("{0} 秒早", -etdSeconds);
			}
			else
			{
				timingPredictionLabel.Text = "定時";
			}
		}
		else
		{
			timeLabel.Text = "#### 秒";
			distancePerTimeLabel.Text = "##.# m/s";
			kmPerHourLabel.Text = "###.# km/h";
			timingPredictionLabel.Text = "### 秒延";
		}
	}
}
