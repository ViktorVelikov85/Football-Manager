-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 04, 2026 at 11:24 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `football_manager`
--

-- --------------------------------------------------------

--
-- Table structure for table `clubs`
--

CREATE TABLE `clubs` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `city` varchar(100) DEFAULT NULL,
  `stadium` varchar(100) DEFAULT NULL,
  `founded_year` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `clubs`
--

INSERT INTO `clubs` (`id`, `name`, `city`, `stadium`, `founded_year`) VALUES
(1, 'Левски София', 'София', 'Стадион Георги Аспарухов', 1914),
(2, 'Лудогорец', 'Разград', 'Хювефарма Арена', 2001),
(3, 'ЦСКА София', 'София', 'Българска армия', 1948),
(4, 'Черно море', 'Варна', 'Тича', 1913),
(5, 'Тест', 'Варна', 'банан', 1999);

-- --------------------------------------------------------

--
-- Table structure for table `leagues`
--

CREATE TABLE `leagues` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `season` varchar(9) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `leagues`
--

INSERT INTO `leagues` (`id`, `name`, `season`) VALUES
(5, 'тест', '1234/5678'),
(6, 'тест2', '1234/5678');

-- --------------------------------------------------------

--
-- Table structure for table `league_teams`
--

CREATE TABLE `league_teams` (
  `league_id` int(11) NOT NULL,
  `club_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `league_teams`
--

INSERT INTO `league_teams` (`league_id`, `club_id`) VALUES
(5, 1),
(5, 2),
(5, 3),
(5, 4),
(5, 5);

-- --------------------------------------------------------

--
-- Table structure for table `matches`
--

CREATE TABLE `matches` (
  `id` int(11) NOT NULL,
  `league_id` int(11) NOT NULL,
  `round_no` int(11) NOT NULL,
  `home_team_id` int(11) NOT NULL,
  `away_team_id` int(11) NOT NULL,
  `home_score` int(11) DEFAULT NULL,
  `away_score` int(11) DEFAULT NULL,
  `match_date` datetime DEFAULT NULL,
  `is_played` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `matches`
--

INSERT INTO `matches` (`id`, `league_id`, `round_no`, `home_team_id`, `away_team_id`, `home_score`, `away_score`, `match_date`, `is_played`) VALUES
(83, 5, 1, 2, 4, 2, 0, '2026-05-09 00:00:00', 1),
(84, 5, 1, 5, 3, 0, 0, '2026-05-09 00:00:00', 1),
(85, 5, 2, 1, 4, 0, 0, '2026-05-16 00:00:00', 1),
(86, 5, 2, 2, 5, 0, 0, '2026-05-16 00:00:00', 1),
(87, 5, 3, 1, 3, NULL, NULL, '2026-05-23 00:00:00', 0),
(88, 5, 3, 4, 5, 0, 0, '2026-05-23 00:00:00', 1),
(89, 5, 4, 1, 5, NULL, NULL, '2026-05-30 00:00:00', 0),
(90, 5, 4, 3, 2, NULL, NULL, '2026-05-30 00:00:00', 0),
(91, 5, 5, 1, 2, NULL, NULL, '2026-06-06 00:00:00', 0),
(92, 5, 5, 3, 4, NULL, NULL, '2026-06-06 00:00:00', 0),
(93, 5, 6, 4, 2, NULL, NULL, '2026-06-13 00:00:00', 0),
(94, 5, 6, 3, 5, NULL, NULL, '2026-06-13 00:00:00', 0),
(95, 5, 7, 4, 1, NULL, NULL, '2026-06-20 00:00:00', 0),
(96, 5, 7, 5, 2, NULL, NULL, '2026-06-20 00:00:00', 0),
(97, 5, 8, 3, 1, NULL, NULL, '2026-06-27 00:00:00', 0),
(98, 5, 8, 5, 4, NULL, NULL, '2026-06-27 00:00:00', 0),
(99, 5, 9, 5, 1, NULL, NULL, '2026-07-04 00:00:00', 0),
(100, 5, 9, 2, 3, NULL, NULL, '2026-07-04 00:00:00', 0),
(101, 5, 10, 2, 1, NULL, NULL, '2026-07-11 00:00:00', 0),
(102, 5, 10, 4, 3, NULL, NULL, '2026-07-11 00:00:00', 0);

-- --------------------------------------------------------

--
-- Table structure for table `match_cards`
--

CREATE TABLE `match_cards` (
  `id` int(11) NOT NULL,
  `match_id` int(11) NOT NULL,
  `player_id` int(11) NOT NULL,
  `card_type` varchar(20) NOT NULL,
  `minute` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `match_cards`
--

INSERT INTO `match_cards` (`id`, `match_id`, `player_id`, `card_type`, `minute`) VALUES
(1, 83, 40, 'Жълт картон', 8);

-- --------------------------------------------------------

--
-- Table structure for table `match_fouls`
--

CREATE TABLE `match_fouls` (
  `id` int(11) NOT NULL,
  `match_id` int(11) NOT NULL,
  `player_id` int(11) NOT NULL,
  `minute` int(11) NOT NULL,
  `foul_type` varchar(50) DEFAULT 'Обикновено'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `match_fouls`
--

INSERT INTO `match_fouls` (`id`, `match_id`, `player_id`, `minute`, `foul_type`) VALUES
(2, 83, 34, 46, 'Обикновено');

-- --------------------------------------------------------

--
-- Table structure for table `match_goals`
--

CREATE TABLE `match_goals` (
  `id` int(11) NOT NULL,
  `match_id` int(11) NOT NULL,
  `player_id` int(11) NOT NULL,
  `club_id` int(11) NOT NULL,
  `minute` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `match_goals`
--

INSERT INTO `match_goals` (`id`, `match_id`, `player_id`, `club_id`, `minute`) VALUES
(1, 83, 5, 2, 1),
(2, 83, 5, 2, 33);

-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE `players` (
  `id` int(11) NOT NULL,
  `club_id` int(11) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `birth_date` date NOT NULL,
  `position` enum('GK','DF','MF','FW') NOT NULL,
  `shirt_number` int(11) DEFAULT NULL,
  `status` enum('Active','Injured','Suspended') DEFAULT 'Active'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `players`
--

INSERT INTO `players` (`id`, `club_id`, `full_name`, `birth_date`, `position`, `shirt_number`, `status`) VALUES
(1, 1, 'Мартин Луков', '1993-07-05', 'GK', 78, 'Active'),
(2, 1, 'Майкон', '1999-02-10', 'DF', 3, 'Active'),
(3, 1, 'Кристиан Макун', '2000-03-05', 'DF', 4, 'Active'),
(4, 3, 'Стипе Вуликич', '2001-01-01', 'DF', 6, 'Active'),
(5, 2, 'Алдаир Невеш', '1999-05-10', 'DF', 21, 'Active'),
(6, 3, 'Карлос Охене', '1992-07-21', 'MF', 8, 'Active'),
(7, 1, 'Асен Митков', '2004-02-16', 'MF', 10, 'Active'),
(8, 1, 'Патрик Мислович', '2001-05-28', 'MF', 23, 'Injured'),
(9, 1, 'Евертон Бала', '1999-01-03', 'MF', 17, 'Active'),
(10, 1, 'Марин Петков', '2003-10-02', 'FW', 88, 'Active'),
(11, 1, 'Мустафа Сангаре', '1998-12-12', 'FW', 12, 'Active'),
(12, 2, 'Хендрик Бонман', '1994-01-19', 'GK', 1, 'Active'),
(13, 3, 'Йоел Андерсон', '1996-11-11', 'DF', 2, 'Active'),
(14, 2, 'Антон Недялков', '1993-04-30', 'DF', 3, 'Active'),
(15, 2, 'Оливие Вердон', '1995-10-05', 'DF', 24, 'Active'),
(16, 2, 'Диниш Алмейда', '1995-06-28', 'DF', 4, 'Active'),
(17, 2, 'Якуб Пьотровски', '1997-10-04', 'MF', 6, 'Active'),
(18, 2, 'Ивайло Чочев', '1993-02-18', 'MF', 18, 'Active'),
(20, 1, 'Кайо Видал', '2000-11-04', 'FW', 11, 'Active'),
(22, 2, 'Ив Ерик Биле', '2004-12-24', 'FW', 29, 'Active'),
(23, 3, 'Густаво Бусато', '1990-08-23', 'GK', 1, 'Active'),
(24, 3, 'Юрген Матей', '1993-04-01', 'DF', 2, 'Active'),
(25, 3, 'Енес Махмутович', '1997-05-22', 'DF', 22, 'Active'),
(26, 3, 'Тибо Вион', '1993-12-11', 'DF', 15, 'Active'),
(27, 3, 'Брадли Де Нойер', '1997-11-13', 'DF', 5, 'Active'),
(28, 3, 'Амос Юга', '1992-12-08', 'MF', 21, 'Active'),
(29, 1, 'Йонатан Линдсет', '1996-02-25', 'MF', 7, 'Active'),
(30, 3, 'Тобиас Хайнц', '1998-07-13', 'MF', 14, 'Active'),
(31, 3, 'Маурисио Гарсес', '1997-07-16', 'FW', 10, 'Active'),
(32, 3, 'Дъкенс Назон', '1994-04-07', 'FW', 9, 'Active'),
(33, 4, 'Геферсон', '1994-07-13', 'DF', 3, 'Active'),
(34, 4, 'Иван Дюлгеров', '1999-07-15', 'GK', 25, 'Active'),
(35, 4, 'Васил Панайотов', '1990-07-16', 'DF', 71, 'Suspended'),
(36, 4, 'Живко Атанасов', '1991-02-03', 'DF', 3, 'Active'),
(37, 4, 'Даниел Димов', '1989-01-21', 'DF', 27, 'Active'),
(38, 4, 'Цветомир Панов', '1990-08-22', 'DF', 2, 'Active'),
(39, 4, 'Мазин Ахмед', '1997-03-10', 'MF', 8, 'Active'),
(40, 4, 'Виктор Попов', '2000-03-05', 'DF', 6, 'Active'),
(41, 4, 'Илиан Илиев', '1999-08-07', 'MF', 10, 'Active'),
(42, 4, 'Дуду', '1997-04-21', 'FW', 11, 'Active'),
(43, 4, 'Матеус Машадо', '1998-06-15', 'FW', 9, 'Active'),
(44, 4, 'Атанас Илиев', '1994-07-09', 'FW', 19, 'Active'),
(45, 4, 'Тест Тестов', '1995-07-14', 'GK', 67, 'Active'),
(49, 5, 'Играч 1', '1999-08-12', 'GK', 1, 'Active'),
(50, 5, 'Играч 2 ', '1999-11-11', 'MF', 2, 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `transfers`
--

CREATE TABLE `transfers` (
  `TransferId` int(11) NOT NULL,
  `PlayerId` int(11) NOT NULL,
  `FromClubId` int(11) DEFAULT NULL,
  `ToClubId` int(11) NOT NULL,
  `TransferDate` datetime NOT NULL,
  `Fee` decimal(15,2) DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transfers`
--

INSERT INTO `transfers` (`TransferId`, `PlayerId`, `FromClubId`, `ToClubId`, `TransferDate`, `Fee`) VALUES
(1, 20, NULL, 1, '2026-03-20 11:40:43', 1.00),
(2, 13, 2, 3, '2026-03-22 11:19:31', 100.00),
(3, 4, 1, 3, '2026-03-22 11:19:31', 100.00),
(4, 29, 3, 1, '2026-03-22 11:21:41', 12345678.00),
(5, 33, 3, 4, '2026-03-22 12:27:25', 123456.21),
(6, 6, 1, 3, '2026-03-22 13:12:48', 35000.00),
(7, 5, 1, 2, '2026-03-30 19:30:16', 50.00);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clubs`
--
ALTER TABLE `clubs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indexes for table `leagues`
--
ALTER TABLE `leagues`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `UQ_LeagueName_Season` (`name`,`season`);

--
-- Indexes for table `league_teams`
--
ALTER TABLE `league_teams`
  ADD PRIMARY KEY (`league_id`,`club_id`),
  ADD KEY `FK_Club` (`club_id`);

--
-- Indexes for table `matches`
--
ALTER TABLE `matches`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_match_league` (`league_id`),
  ADD KEY `fk_match_home` (`home_team_id`),
  ADD KEY `fk_match_away` (`away_team_id`);

--
-- Indexes for table `match_cards`
--
ALTER TABLE `match_cards`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_cards_match` (`match_id`),
  ADD KEY `fk_cards_player` (`player_id`);

--
-- Indexes for table `match_fouls`
--
ALTER TABLE `match_fouls`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_fouls_match` (`match_id`),
  ADD KEY `fk_fouls_player` (`player_id`);

--
-- Indexes for table `match_goals`
--
ALTER TABLE `match_goals`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_goals_match` (`match_id`),
  ADD KEY `fk_goals_player` (`player_id`),
  ADD KEY `fk_goals_club` (`club_id`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_player_club` (`club_id`);

--
-- Indexes for table `transfers`
--
ALTER TABLE `transfers`
  ADD PRIMARY KEY (`TransferId`),
  ADD KEY `PlayerId` (`PlayerId`),
  ADD KEY `FromClubId` (`FromClubId`),
  ADD KEY `ToClubId` (`ToClubId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `clubs`
--
ALTER TABLE `clubs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `leagues`
--
ALTER TABLE `leagues`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `matches`
--
ALTER TABLE `matches`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=103;

--
-- AUTO_INCREMENT for table `match_cards`
--
ALTER TABLE `match_cards`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `match_fouls`
--
ALTER TABLE `match_fouls`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `match_goals`
--
ALTER TABLE `match_goals`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `players`
--
ALTER TABLE `players`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=51;

--
-- AUTO_INCREMENT for table `transfers`
--
ALTER TABLE `transfers`
  MODIFY `TransferId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `league_teams`
--
ALTER TABLE `league_teams`
  ADD CONSTRAINT `FK_Club` FOREIGN KEY (`club_id`) REFERENCES `clubs` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_League` FOREIGN KEY (`league_id`) REFERENCES `leagues` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `matches`
--
ALTER TABLE `matches`
  ADD CONSTRAINT `fk_match_away` FOREIGN KEY (`away_team_id`) REFERENCES `clubs` (`id`),
  ADD CONSTRAINT `fk_match_home` FOREIGN KEY (`home_team_id`) REFERENCES `clubs` (`id`),
  ADD CONSTRAINT `fk_match_league` FOREIGN KEY (`league_id`) REFERENCES `leagues` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `match_cards`
--
ALTER TABLE `match_cards`
  ADD CONSTRAINT `fk_cards_match` FOREIGN KEY (`match_id`) REFERENCES `matches` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_cards_player` FOREIGN KEY (`player_id`) REFERENCES `players` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `match_fouls`
--
ALTER TABLE `match_fouls`
  ADD CONSTRAINT `fk_fouls_match` FOREIGN KEY (`match_id`) REFERENCES `matches` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_fouls_player` FOREIGN KEY (`player_id`) REFERENCES `players` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `match_goals`
--
ALTER TABLE `match_goals`
  ADD CONSTRAINT `fk_goals_club` FOREIGN KEY (`club_id`) REFERENCES `clubs` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_goals_match` FOREIGN KEY (`match_id`) REFERENCES `matches` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_goals_player` FOREIGN KEY (`player_id`) REFERENCES `players` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `players`
--
ALTER TABLE `players`
  ADD CONSTRAINT `fk_player_club` FOREIGN KEY (`club_id`) REFERENCES `clubs` (`id`);

--
-- Constraints for table `transfers`
--
ALTER TABLE `transfers`
  ADD CONSTRAINT `transfers_ibfk_1` FOREIGN KEY (`PlayerId`) REFERENCES `players` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `transfers_ibfk_2` FOREIGN KEY (`FromClubId`) REFERENCES `clubs` (`id`) ON DELETE SET NULL,
  ADD CONSTRAINT `transfers_ibfk_3` FOREIGN KEY (`ToClubId`) REFERENCES `clubs` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
