using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SweetSugar.Scripts.Core;



public class AchievementManager : MonoBehaviour
{
	public static AchievementManager instance;

	private void Awake()
	{
		instance = this;
	}

	private void Update()
	{
		if (LevelManager.Score > PlayerPrefs.GetInt("Score" + PlayerPrefs.GetInt("OpenLevel")))
		{
			PlayerPrefs.SetInt("Score" + PlayerPrefs.GetInt("OpenLevel"), LevelManager.Score);
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Debug.Log(Achievements.scoreAchievement);
		}

		checkScoreAchievement(Achievements.scoreAchievement);
	}


	public void checkFreeMoveAchivement(int count)
	{
		// No of times free moves used to unlock achievements.
		switch (count)
		{
			case 5:
				if (!Achievements.unlockedFreeMoveUsed5X)
				{
					Achievements.unlockedFreeMoveUsed5X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesFreeMoveUsed5X);
				}
				break;

			case 25:
				if (!Achievements.unlockedFreeMoveUsed25X)
				{
					Achievements.unlockedFreeMoveUsed25X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesFreeMoveUsed25X);
				}
				break;

			case 50:
				if (!Achievements.unlockedFreeMoveUsed50X)
				{
					Achievements.unlockedFreeMoveUsed50X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesFreeMoveUsed50X);
				}
				break;

		}

	}


	public void checkMultiColorAchivement(int count)
	{
		// No of times free moves used to unlock achievements.
		switch (count)
		{
			case 5:
				if (!Achievements.unlockedMulticolorCandyUsed5X)
				{
					Achievements.unlockedMulticolorCandyUsed5X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDmulticolorCandyUsed5X);
				}
				break;

			case 25:
				if (!Achievements.unlockedMulticolorCandyUsed25X)
				{
					Achievements.unlockedMulticolorCandyUsed25X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDmulticolorCandyUsed25X);
				}
				break;

			case 50:
				if (!Achievements.unlockedMulticolorCandyUsed50X)
				{
					Achievements.unlockedMulticolorCandyUsed50X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDmulticolorCandyUsed50X);
				}
				break;

		}

	}


	public void checkExtraMovesAchivement(int count)
	{
		// No of times free moves used to unlock achievements.
		switch (count)
		{
			case 5:
				if (!Achievements.unlockedExtraMovesUsed5X)
				{
					Achievements.unlockedExtraMovesUsed5X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDextraMovesUsed5X);
				}
				break;

			case 25:
				if (!Achievements.unlockedExtraMovesUsed25X)
				{
					Achievements.unlockedExtraMovesUsed25X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDextraMovesUsed25X);
				}
				break;

			case 50:
				if (!Achievements.unlockedExtraMovesUsed50X)
				{
					Achievements.unlockedExtraMovesUsed50X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDextraMovesUsed50X);
				}
				break;

		}

	}


	public void checkExtraTimeAchivement(int count)
	{
		// No of times free moves used to unlock achievements.
		switch (count)
		{
			case 5:
				if (!Achievements.unlockedExtraTimeUsed5X)
				{
					Achievements.unlockedExtraTimeUsed5X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDextraTimeUsed5X);
				}
				break;

			case 25:
				if (!Achievements.unlockedExtraTimeUsed25X)
				{
					Achievements.unlockedExtraTimeUsed25X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDextraTimeUsed25X);
				}
				break;

			case 50:
				if (!Achievements.unlockedExtraTimeUsed50X)
				{
					Achievements.unlockedExtraTimeUsed50X = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDextraTimeUsed50X);
				}
				break;

		}

	}

	public void checkLevelAchievement(int count)
	{

		switch (count)
		{
			case 5:
				//GooglePlayService.instance.increaseAchievement(Achievements.IDlevelAchievement, 1);
				
				break;

			case 10:
				//GooglePlayService.instance.increaseAchievement(Achievements.IDlevelAchievement, 1);
				
				break;

			case 20:
				//GooglePlayService.instance.increaseAchievement(Achievements.IDlevelAchievement, 1);
				
				break;

		}


	}

	public void checkScoreAchievement(int count)
	{

		if (count > 1000)
		{
			if (count > 2000)
			{
				if(count > 3000)
				{
					if(count > 4000)
					{
						if (count > 5000)
						{
							if (!Achievements.unlockedScoreAchievement5st)
							{
								Debug.Log("achieved 1");
								Achievements.unlockedScoreAchievement5st = true;
								//GooglePlayService.instance.achievementCompleted(Achievements.IDScoreAchievement5st);
							}
						}

						if (!Achievements.unlockedScoreAchievement4st)
						{
							Debug.Log("achieved 2");
							Achievements.unlockedScoreAchievement4st = true;
							//GooglePlayService.instance.achievementCompleted(Achievements.IDScoreAchievement4st);
						}

					}

					if (!Achievements.unlockedScoreAchievement3st)
					{
						Achievements.unlockedScoreAchievement3st = true;
						//GooglePlayService.instance.achievementCompleted(Achievements.IDScoreAchievement3st);
					}

				}

				if (!Achievements.unlockedScoreAchievement2st)
				{
					Achievements.unlockedScoreAchievement2st = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDScoreAchievement2st);
				}

			}

			if (!Achievements.unlockedScoreAchievement1st)
			{
				Achievements.unlockedScoreAchievement1st = true;
				//GooglePlayService.instance.achievementCompleted(Achievements.IDScoreAchievement1st);
			}

		}


		#region old code
		//if (count > 500 && count < 1000)
		//{
		//	if (!Achievements.unlockedScoreAchievement1st)
		//	{
		//		Achievements.unlockedScoreAchievement1st = true;
		//		//GooglePlayService.instance.achievementCompleted(Achievements.IDScoreAchievement1st);
		//	}
		//}

		//if (count > 1000 && count < 2000)
		//{
		//	if (!Achievements.unlockedScoreAchievement2st)
		//	{
		//		Achievements.unlockedScoreAchievement2st = true;
		//		//GooglePlayService.instance.achievementCompleted(Achievements.IDScoreAchievement2st);
		//	}
		//}

		//if (count > 2000 && count < 3000)
		//{
		//	if (!Achievements.unlockedScoreAchievement3st)
		//	{
		//		Achievements.unlockedScoreAchievement3st = true;
		//		//GooglePlayService.instance.achievementCompleted(Achievements.IDScoreAchievement3st);
		//	}
		//}

		//if (count > 3000 && count < 4000)
		//{
		//	if (!Achievements.unlockedScoreAchievement4st)
		//	{
		//		Achievements.unlockedScoreAchievement4st = true;
		//		//GooglePlayService.instance.achievementCompleted(Achievements.IDScoreAchievement4st);
		//	}
		//}

		#endregion


	}


	public void checksolidBlockAchievement(int count)
	{

		if (count > 10)
		{
			if (count > 20)
			{
				if (count > 30)
				{
					if (count > 40)
					{
						if (!Achievements.unlockedSolidBLock4)
						{
							Debug.Log("achieved 4");
							Achievements.unlockedSolidBLock4 = true;
							//GooglePlayService.instance.achievementCompleted(Achievements.IDsolidBlockAchievement4);
						}
					}

					if (!Achievements.unlockedSolidBLock3)
					{
						Debug.Log("achieved 3");
						Achievements.unlockedSolidBLock3 = true;
						//GooglePlayService.instance.achievementCompleted(Achievements.IDsolidBlockAchievement3);
					}

				}

				if (!Achievements.unlockedSolidBLock2)
				{
					Achievements.unlockedSolidBLock2 = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDsolidBlockAchievement2);
				}

			}

			if (!Achievements.unlockedSolidBLock1)
			{
				Achievements.unlockedSolidBLock1 = true;
				//GooglePlayService.instance.achievementCompleted(Achievements.IDsolidBlockAchievement1);
			}

		}


	}

	public void checklevelFailedAchievement(int count)
	{

		if (count > 10)
		{
			if (count > 20)
			{
				if (count > 30)
				{
					if (count > 40)
					{
						if (!Achievements.unlockedLevelFailed4)
						{
							Debug.Log("achieved 4");
							Achievements.unlockedLevelFailed4 = true;
							//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesLevelFailed4);
						}
					}

					if (!Achievements.unlockedLevelFailed3)
					{
						Debug.Log("achieved 3");
						Achievements.unlockedLevelFailed3 = true;
						//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesLevelFailed3);
					}

				}

				if (!Achievements.unlockedLevelFailed2)
				{
					Achievements.unlockedLevelFailed2 = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesLevelFailed2);
				}

			}

			if (!Achievements.unlockedLevelFailed1)
			{
				Achievements.unlockedLevelFailed1 = true;
				//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesLevelFailed1);
			}

		}

	}
	
	public void checksugarSquareAchievement(int count)
	{

		if (count > 10)
		{
			if (count > 20)
			{
				if (count > 30)
				{
					if (count > 40)
					{
						if (!Achievements.unlockedsugarSquareAchievement4)
						{
							Debug.Log("achieved 4");
							Achievements.unlockedsugarSquareAchievement4 = true;
							//GooglePlayService.instance.achievementCompleted(Achievements.IDsugarSquareAchievment4);
						}
					}

					if (!Achievements.unlockedsugarSquareAchievement3)
					{
						Debug.Log("achieved 3");
						Achievements.unlockedsugarSquareAchievement3 = true;
						//GooglePlayService.instance.achievementCompleted(Achievements.IDsugarSquareAchievment3);
					}

				}

				if (!Achievements.unlockedsugarSquareAchievement2)
				{
					Achievements.unlockedsugarSquareAchievement2 = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDsugarSquareAchievment2);
				}

			}

			if (!Achievements.unlockedsugarSquareAchievement1)
			{
				Achievements.unlockedsugarSquareAchievement1 = true;
				//GooglePlayService.instance.achievementCompleted(Achievements.IDsugarSquareAchievment1);
			}

		}


	}

	public void checkChocoAchievement(int count)
	{

		if (count > 10)
		{
			if (count > 20)
			{
				if (count > 30)
				{
					if (count > 40)
					{
						if (!Achievements.unlockedChocolateAchievement4)
						{
							Debug.Log("achieved 4");
							Achievements.unlockedChocolateAchievement4 = true;
							//GooglePlayService.instance.achievementCompleted(Achievements.IDChocoAchievment4);
						}
					}

					if (!Achievements.unlockedChocolateAchievement3)
					{
						Debug.Log("achieved 3");
						Achievements.unlockedChocolateAchievement3 = true;
						//GooglePlayService.instance.achievementCompleted(Achievements.IDChocoAchievment3);
					}

				}

				if (!Achievements.unlockedChocolateAchievement2)
				{
					Achievements.unlockedChocolateAchievement2 = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDChocoAchievment2);
				}

			}

			if (!Achievements.unlockedChocolateAchievement1)
			{
				Achievements.unlockedChocolateAchievement1 = true;
				//GooglePlayService.instance.achievementCompleted(Achievements.IDChocoAchievment1);
			}

		}


	}



	public void checkdailyBonusAchievement(int count)
	{

		if (count > 10)
		{
			if (count > 20)
			{
				if (count > 30)
				{
					if (count > 40)
					{
						if (!Achievements.unlockedDailyBonus4)
						{
							Debug.Log("achieved 4");
							Achievements.unlockedDailyBonus4 = true;
							//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesDailyBonus4);
						}
					}

					if (!Achievements.unlockedDailyBonus3)
					{
						Debug.Log("achieved 3");
						Achievements.unlockedDailyBonus3 = true;
						//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesDailyBonus3);
					}

				}

				if (!Achievements.unlockedDailyBonus2)
				{
					Achievements.unlockedDailyBonus2 = true;
					//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesDailyBonus2);
				}

			}

			if (!Achievements.unlockedDailyBonus1)
			{
				Achievements.unlockedDailyBonus1 = true;
				//GooglePlayService.instance.achievementCompleted(Achievements.IDtimesDailyBonus1);
			}

		}

	}



}
