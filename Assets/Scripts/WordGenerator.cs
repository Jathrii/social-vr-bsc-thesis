﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SocialVR
{
    public class WordGenerator : MonoBehaviour
    {
        private static string[] words = new string[]
            {
                "manager",
                "contemporary",
                "other",
                "provision",
                "knock",
                "hover",
                "cemetery",
                "chance",
                "houseplant",
                "ex",
                "match",
                "intention",
                "finance",
                "explain",
                "wagon",
                "regulation",
                "lake",
                "preoccupation",
                "physical",
                "unique",
                "engineer",
                "strict",
                "pepper",
                "detail",
                "familiar",
                "matrix",
                "agenda",
                "swop",
                "fireplace",
                "ear",
                "superior",
                "mirror",
                "plead",
                "computing",
                "grass",
                "profile",
                "rotten",
                "salon",
                "judgment",
                "exemption",
                "agriculture",
                "pour",
                "seminar",
                "record",
                "panic",
                "impress",
                "requirement",
                "cousin",
                "expansion",
                "bubble",
                "great",
                "safari",
                "sample",
                "chimpanzee",
                "displace",
                "registration",
                "allowance",
                "negotiation",
                "runner",
                "inn",
                "right",
                "literacy",
                "AIDS",
                "church",
                "dream",
                "kit",
                "front",
                "welcome",
                "string",
                "plaster",
                "flavor",
                "commerce",
                "pierce",
                "fight",
                "spring",
                "title",
                "pleasure",
                "capital",
                "medieval",
                "deck",
                "beer",
                "gravel",
                "crowd",
                "limit",
                "meadow",
                "lid",
                "arm",
                "rape",
                "bee",
                "chain",
                "tribe",
                "bulletin",
                "injection",
                "reflect",
                "tribute",
                "forget",
                "foot",
                "entitlement",
                "show",
                "truck",
                "colleague",
                "scrape",
                "franchise",
                "die",
                "entertain",
                "initial",
                "monk",
                "intensify",
                "appendix",
                "creed",
                "pastel",
                "society",
                "polite",
                "answer",
                "casualty",
                "peace",
                "stretch",
                "paper",
                "review",
                "acceptable",
                "text",
                "arrest",
                "weak",
                "random",
                "operation",
                "shame",
                "rate",
                "crouch",
                "conscience",
                "grimace",
                "tree",
                "miserable",
                "portion",
                "wonder",
                "trick",
                "crackpot",
                "decline",
                "incentive",
                "monster",
                "salvation",
                "prosecution",
                "pardon",
                "difference",
                "justify",
                "exhibition",
                "adopt",
                "addicted",
                "future",
                "minority",
                "sum",
                "housewife",
                "polish",
                "heavy",
                "average",
                "irony",
                "salesperson",
                "directory",
                "error",
                "format",
                "jockey",
                "drop",
                "marriage",
                "swing",
                "vain",
                "anticipation",
                "extort",
                "slide",
                "reluctance",
                "element",
                "land",
                "carbon",
                "whip",
                "station",
                "owe",
                "countryside",
                "fresh",
                "prize",
                "character",
                "soprano",
                "giant",
                "wedding",
                "spin",
                "lily",
                "guarantee",
                "bake",
                "greet",
                "urine",
                "nomination",
                "damn",
                "testify",
                "socialist",
                "law",
                "protection",
                "warm",
                "blade",
                "rhetoric",
                "invite",
                "relief",
                "hemisphere",
                "headquarters",
                "curve",
                "unrest",
                "hypothesize",
                "collection",
                "vehicle",
                "bomb",
                "venture",
                "feel",
                "trial",
                "wing",
                "lick",
                "appeal",
                "camera",
                "delicate",
                "rain",
                "popular",
                "movement",
                "plot",
                "variant",
                "leaf",
                "guest",
                "studio",
                "pest",
                "confession",
                "cycle",
                "twilight",
                "soldier",
                "realize",
                "fund",
                "lodge",
                "fisherman",
                "year",
                "navy",
                "examination",
                "fine",
                "psychology",
                "depart",
                "tone",
                "wrist",
                "despair",
                "profit",
                "disgrace",
                "coast",
                "fall",
                "chicken",
                "pull",
                "explosion",
                "worm",
                "overcharge",
                "captivate",
                "computer virus",
                "exception",
                "thread",
                "feign",
                "corruption",
                "visible",
                "soft",
                "member",
                "bet",
                "sow",
                "lie",
                "broadcast",
                "undertake",
                "electronics",
                "convert",
                "desert",
                "snow",
                "weakness",
                "lane",
                "shallow",
                "regard",
                "performance",
                "maximum",
                "canvas",
                "friendly",
                "steward",
                "porter",
                "market",
                "defend",
                "full",
                "drive",
                "lift",
                "attachment",
                "helpless",
                "program",
                "machinery",
                "scan",
                "evaluate",
                "insure",
                "stop",
                "lump",
                "preference",
                "soil",
                "far",
                "basis",
                "trunk",
                "pile",
                "royalty",
                "folklore",
                "trainer",
                "influence",
                "herd",
                "dog",
                "struggle",
                "mathematics",
                "tail",
                "mug",
                "leftovers",
                "write",
                "metal",
                "air",
                "slam",
                "coat",
                "feast",
                "citizen",
                "comedy",
                "compromise",
                "aisle",
                "rest",
                "evoke",
                "cater",
                "contact",
                "plain",
                "block",
                "car",
                "apology",
                "smash",
                "wrong",
                "crevice",
                "hammer",
                "bridge",
                "subway",
                "glasses",
                "committee",
                "parking",
                "middle",
                "sail",
                "belt",
                "deputy",
                "obese",
                "chimney",
                "speculate",
                "order",
                "drag",
                "squash",
                "fan",
                "result",
                "fixture",
                "rank",
                "scholar",
                "hurl",
                "redundancy",
                "dance",
                "asylum",
                "release",
                "petty",
                "afford",
                "gallery",
                "econobox",
                "mosque",
                "statement",
                "generate",
                "reserve",
                "ring",
                "harmony",
                "threaten",
                "glimpse",
                "survey",
                "battery",
                "page",
                "agent",
                "sketch",
                "key",
                "fountain",
                "eavesdrop",
                "hut",
                "notorious",
                "shot",
                "open",
                "tap",
                "original",
                "bloodshed",
                "admit",
                "pit",
                "trouble",
                "professional",
                "process",
                "circumstance",
                "contradiction",
                "concept",
                "jealous",
                "system",
                "instrument",
                "cheap",
                "impulse",
                "seller",
                "summary",
                "dragon",
                "glow",
                "wilderness",
                "finger",
                "tempt",
                "repeat",
                "main",
                "cast",
                "exploit",
                "body",
                "excitement",
                "Sunday",
                "date",
                "scene",
                "kitchen",
                "mushroom",
                "horse",
                "assault",
                "see",
                "aware",
                "highlight",
                "nerve",
                "vegetarian",
                "immune",
                "inflate",
                "tropical",
                "lost",
                "abolish",
                "calf",
                "dimension",
                "reduce",
                "property",
                "enhance",
                "chalk",
                "fog",
                "onion",
                "breakdown",
                "snack",
                "decorative",
                "worth",
                "radio",
                "colorful",
                "sweater",
                "shaft",
                "bottom",
                "bowel",
                "expect",
                "young",
                "bow",
                "continuous",
                "ankle",
                "miss",
                "lineage",
                "obstacle",
                "perfect",
                "keep",
                "tower",
                "possible",
                "pursuit",
                "inhabitant",
                "proof",
                "establish",
                "boot",
                "hiccup",
                "head",
                "produce",
                "breeze",
                "domestic",
                "module",
                "cap",
                "veteran",
                "customer",
                "wheel",
                "monstrous",
                "replacement",
                "mosquito",
                "suffer",
                "diagram",
                "break down",
                "ball",
                "major",
                "acute",
                "cancel",
                "distance",
                "temperature",
                "rumor",
                "kneel",
                "teenager",
                "size",
                "infinite",
                "coal",
                "low",
                "diplomat",
                "profession",
                "sink",
                "verdict",
                "occupation",
                "van",
                "sanctuary",
                "support",
                "gate",
                "meeting",
                "principle",
                "guideline",
                "kill",
                "butterfly",
                "last",
                "crutch",
                "progressive",
                "scramble",
                "watch",
                "summer",
                "anger",
                "long",
                "railcar",
                "candle",
                "blame",
                "disturbance",
                "cruel",
                "sting",
                "gasp",
                "beat",
                "vegetation",
                "example",
                "reactor",
                "equip",
                "message",
                "slump",
                "compact",
                "provide",
                "enthusiasm",
                "monopoly",
                "unlike",
                "collapse",
                "winter",
                "tenant",
                "panel",
                "city",
                "thoughtful",
                "mountain",
                "decoration",
                "cotton",
                "extent",
                "partner",
                "hand",
                "genetic",
                "meaning",
                "rugby",
                "alarm",
                "ward",
                "south",
                "abnormal",
                "remain",
                "cabin",
                "ceiling",
                "password",
                "mainstream",
                "score",
                "chauvinist",
                "ivory",
                "excavate",
                "factory",
                "thrust",
                "drill",
                "reduction",
                "blank",
                "exceed",
                "filter",
                "round",
                "ant",
                "column",
                "absence",
                "amber",
                "craftsman",
                "chest",
                "bolt",
                "impound",
                "forestry",
                "declaration",
                "shelter",
                "ego",
                "facade",
                "harbor",
                "remember",
                "composer",
                "era",
                "achieve",
                "swallow",
                "amuse",
                "nominate",
                "movie",
                "enfix",
                "pool",
                "package",
                "portrait",
                "marble",
                "pattern",
                "fuel",
                "banquet",
                "graphic",
                "shake",
                "explicit",
                "dive",
                "dribble",
                "pain",
                "bare",
                "interface",
                "fork",
                "dairy",
                "dramatic",
                "tasty",
                "opera",
                "design",
                "creation",
                "experience",
                "hear",
                "command",
                "bland",
                "infrastructure",
                "integrity",
                "suppress",
                "say",
                "shareholder",
                "sandwich",
                "relaxation",
                "retired",
                "celebration",
                "subject",
                "trace",
                "wrestle",
                "moral",
                "bar",
                "priority",
                "snuggle",
                "doubt",
                "toll",
                "aloof",
                "sword",
                "forecast",
                "fruit",
                "strong",
                "silence",
                "reporter",
                "elite",
                "disorder",
                "abandon",
                "residence",
                "present",
                "choose",
                "vein",
                "offer",
                "lamb",
                "restrain",
                "flexible",
                "gain",
                "gown",
                "concrete",
                "salt",
                "constraint",
                "sheet",
                "horoscope",
                "due",
                "equal",
                "slime",
                "address",
                "reproduction",
                "disco",
                "deliver",
                "test",
                "academy",
                "swarm",
                "concern",
                "discipline",
                "beach",
                "offender",
                "gear",
                "child",
                "presence",
                "easy",
                "game",
                "pop",
                "location",
                "blackmail",
                "industry",
                "introduction",
                "contrast",
                "economics",
                "doctor",
                "failure",
                "relate",
                "creep",
                "pie",
                "carrot",
                "glass",
                "waste",
                "imagine",
                "mutation",
                "beam",
                "beneficiary",
                "wake",
                "leaflet",
                "information",
                "halt",
                "prisoner",
                "bay",
                "cover",
                "exploration",
                "award",
                "treaty",
                "compete",
                "normal",
                "affair",
                "graze",
                "tournament",
                "request",
                "part",
                "sympathetic",
                "designer",
                "image",
                "arrangement",
                "advertise",
                "style",
                "prejudice",
                "archive",
                "global",
                "spy",
                "plane",
                "regular",
                "eye",
                "action",
                "origin",
                "village",
                "wage",
                "exaggerate",
                "loose",
                "terrace",
                "upset",
                "map",
                "tiger",
                "lamp",
                "training",
                "second",
                "dismissal",
                "method",
                "orientation",
                "pioneer",
                "accept",
                "burial",
                "passion",
                "production",
                "ecstasy",
                "slave",
                "vacuum",
                "accurate",
                "Venus",
                "partnership",
                "risk",
                "official",
                "characteristic",
                "essay",
                "step",
                "disk",
                "book",
                "nose",
                "break",
                "deprive",
                "combine",
                "penny",
                "silk",
                "flesh",
                "particle",
                "nonremittal",
                "traction",
                "stunning",
                "bother",
                "distant",
                "copy",
                "red",
                "gradient",
                "relation",
                "cathedral",
                "joystick",
                "ditch",
                "nervous",
                "advantage",
                "ready",
                "bitch",
                "indication",
                "absolute",
                "introduce",
                "broccoli",
                "trench",
                "galaxy",
                "agony",
                "publicity",
                "advertising",
                "opposition",
                "hard",
                "cook",
                "cheat",
                "feedback",
                "trait",
                "pound",
                "morsel",
                "pumpkin",
                "convenience",
                "bathroom",
                "tender",
                "seat",
                "well",
                "uncle",
                "medal",
                "dose",
                "passage",
                "conspiracy",
                "breed",
                "credit",
                "government",
                "credibility",
                "secular",
                "bear",
                "army",
                "climb",
                "rich",
                "brother",
                "bless",
                "urge",
                "gregarious",
                "nuclear",
                "implicit",
                "scratch",
                "chip",
                "brink",
                "tease",
                "illusion",
                "fool",
                "essential",
                "salmon",
                "sofa",
                "noise",
                "rough",
                "emergency",
                "feeling",
                "finish",
                "constitutional",
                "exclude",
                "migration",
                "attitude",
                "breathe",
                "shed",
                "career",
                "court",
                "crosswalk",
                "prediction",
                "office",
                "muggy",
                "learn",
                "reliance",
                "post",
                "rare",
                "country",
                "summit",
                "unpleasant",
                "withdrawal",
                "overview",
                "gradual",
                "single",
                "nuance",
                "conservation",
                "speed",
                "fee",
                "organisation",
                "mark",
                "slice",
                "kinship",
                "linger",
                "comfortable",
                "band",
                "concession",
                "benefit",
                "cultivate",
                "motivation",
                "black",
                "sticky",
                "rainbow",
                "ambiguity",
                "campaign",
                "rally",
                "permission",
                "follow",
                "escape",
                "communication",
                "spirit",
                "grant",
                "issue",
                "delivery",
                "license",
                "handy",
                "hope",
                "reinforce",
                "surface",
                "throw",
                "assertive",
                "begin",
                "condition",
                "brake",
                "locate",
                "section",
                "find",
                "strikebreaker",
                "race",
                "node",
                "fun",
                "tear",
                "owl",
                "inject",
                "frequency",
                "qualify",
                "reception",
                "code",
                "study",
                "authority",
                "cash",
                "predator",
                "drama",
                "punch",
                "activate",
                "short",
                "leadership",
                "governor",
                "cup",
                "layer",
                "necklace",
                "core",
                "exchange",
                "fool around",
                "link",
                "cheese",
                "lounge",
                "evening",
                "bury",
                "thought",
                "confusion",
                "straighten",
                "wound",
                "tie",
                "quiet",
                "rack",
                "cattle",
                "classify",
                "cane",
                "carpet",
                "ancestor",
                "analysis",
                "mechanical",
                "accompany",
                "quote",
                "variation",
                "material",
                "congress",
                "revolutionary",
                "constituency",
                "holiday",
                "moon",
                "advocate",
                "demonstrator",
                "hair",
                "secure",
                "clay",
                "girlfriend",
                "height",
                "go",
                "prevent",
                "lion",
                "define",
                "river",
                "reasonable",
                "association",
                "shout",
                "bride",
                "endorse",
                "fish",
                "inquiry",
                "screw",
                "coverage",
                "carve",
                "continuation",
                "stereotype",
                "solo",
                "axis",
                "lonely",
                "funeral",
                "anxiety",
                "guerrilla",
                "wander",
                "camp",
                "mobile",
                "embarrassment",
                "egg",
                "invisible",
                "deprivation",
                "referral",
                "fleet"
            };

        public static string Generate()
        {
            return words[Random.Range(0, words.Length)];
        }
    }
}
