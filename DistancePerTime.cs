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
	private Label kmPerHourTitleLabel, kmPerHourlabel;
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
		distanceTitleLabel.Text = "次のポイントまでの距離";
		distanceTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
		distanceLabel = CreateControl<Label>(mainPanel, 0, 1, panelWidth, 2);
		distanceLabel.Font = doubleFont;
		distanceLabel.Text = "#### m";
		distanceLabel.TextAlign = ContentAlignment.MiddleRight;

		timeTitleLabel = CreateControl<Label>(mainPanel, 0, 3, panelWidth, 1);
		timeTitleLabel.Font = normalFont;
		timeTitleLabel.Text = "次のポイントまでの時間";
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
		kmPerHourlabel = CreateControl<Label>(mainPanel, 0, 10, panelWidth, 2);
		kmPerHourlabel.Font = doubleFont;
		kmPerHourlabel.Text = "###.# km/h";
		kmPerHourlabel.TextAlign = ContentAlignment.MiddleRight;

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
	}
}
