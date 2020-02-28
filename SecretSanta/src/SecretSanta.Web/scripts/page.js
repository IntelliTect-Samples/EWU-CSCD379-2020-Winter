"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
var secretsanta_client_1 = require("./secretsanta-client");
var App = /** @class */ (function () {
    function App() {
    }
    App.prototype.getAllUsers = function () {
        return __awaiter(this, void 0, void 0, function () {
            var userClient, users;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        userClient = new secretsanta_client_1.UserClient();
                        return [4 /*yield*/, userClient.getAll()];
                    case 1:
                        users = _a.sent();
                        return [2 /*return*/, users];
                }
            });
        });
    };
    App.prototype.getAllGroups = function () {
        return __awaiter(this, void 0, void 0, function () {
            var groupClient, groups;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        groupClient = new secretsanta_client_1.GroupClient();
                        return [4 /*yield*/, groupClient.getAll()];
                    case 1:
                        groups = _a.sent();
                        return [2 /*return*/, groups];
                }
            });
        });
    };
    App.prototype.getAllGifts = function () {
        return __awaiter(this, void 0, void 0, function () {
            var giftClient, gifts;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        giftClient = new secretsanta_client_1.GiftClient();
                        return [4 /*yield*/, giftClient.getAll()];
                    case 1:
                        gifts = _a.sent();
                        return [2 /*return*/, gifts];
                }
            });
        });
    };
    App.prototype.addAllGifts = function (giftArray) {
        return __awaiter(this, void 0, void 0, function () {
            var giftClient;
            return __generator(this, function (_a) {
                giftClient = new secretsanta_client_1.GiftClient();
                giftArray.forEach(function (gift) {
                    giftClient.post(gift);
                });
                return [2 /*return*/];
            });
        });
    };
    App.prototype.deleteAllGifts = function () {
        return __awaiter(this, void 0, void 0, function () {
            var giftClient;
            return __generator(this, function (_a) {
                giftClient = new secretsanta_client_1.GiftClient();
                getAllGifts().then(function (giftArray) {
                    giftArray.forEach(function (gift) {
                        giftClient.delete(gift.id);
                    });
                });
                return [2 /*return*/];
            });
        });
    };
    return App;
}());
exports.App = App;
function getAllUsers() {
    return __awaiter(this, void 0, void 0, function () {
        var userClient, users;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    userClient = new secretsanta_client_1.UserClient();
                    return [4 /*yield*/, userClient.getAll()];
                case 1:
                    users = _a.sent();
                    return [2 /*return*/, users];
            }
        });
    });
}
exports.getAllUsers = getAllUsers;
function getAllGroups() {
    return __awaiter(this, void 0, void 0, function () {
        var groupClient, groups;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    groupClient = new secretsanta_client_1.GroupClient();
                    return [4 /*yield*/, groupClient.getAll()];
                case 1:
                    groups = _a.sent();
                    return [2 /*return*/, groups];
            }
        });
    });
}
exports.getAllGroups = getAllGroups;
function getAllGifts() {
    return __awaiter(this, void 0, void 0, function () {
        var giftClient, gifts;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    giftClient = new secretsanta_client_1.GiftClient();
                    return [4 /*yield*/, giftClient.getAll()];
                case 1:
                    gifts = _a.sent();
                    return [2 /*return*/, gifts];
            }
        });
    });
}
exports.getAllGifts = getAllGifts;
//# sourceMappingURL=page.js.map