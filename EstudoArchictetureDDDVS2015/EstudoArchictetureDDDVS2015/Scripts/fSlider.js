(function($) {
    $.fn.fSlider = function (options, optionName, arg1, arg2) {
        return this.each(function (eachIndex, item) {
            var fCP;

            if (typeof options == "string") {
                fCP = $(item).data("fSlider");

                if (fCP == undefined) return;

                if (options === "option") {
                    if (optionName === "moveNext") {
                        fCP.moveNext();
                        return;
                    }
                    if (optionName === "movePrevious") {
                        fCP.movePrevious();
                        return;
                    }
                    if (optionName === "moveToId") {
                        fCP.moveToId(arg1);
                        return;
                    }
                    if (optionName === "moveToIndex" && typeof arg1 == "number") {
                        fCP.moveToIndex(parseInt(arg1));
                        return;
                    }
                    if (optionName === "setIndexClicable" && typeof arg1 == "number" || typeof arg2 === "boolean") {
                        return;
                    }
                    if (optionName === "setIdClicable" && typeof arg1 == "string" && typeof arg2 === "boolean") {
                        return;
                    }
                }

                alert("Erro de Configuração");
            }

            if (options.width != undefined) {
                fCP = new fSlider(options, item);
                $(item).data("fSlider", fCP);
                return;
            }

            alert("Erro de Configuração");
        });
    };
} (jQuery));

function fSlider(options, mainDiv) {
    var cp = this;

    (function($) {

        // -- fields ----------------------------------------------------------------------

        cp.jMainDiv = $(mainDiv);

        cp.jMainBodyDiv = cp.jMainDiv.children(".fSliderBody");

        cp.jSlider = cp.jMainBodyDiv.children(".slider");

        cp.jMainBarDiv = $(document.createElement("div"));

        cp.settings = $.extend({
            width: 1000,
            effectTime: 1000,
            index: 0
        }, options);

        cp.blockWidth = (cp.settings.width - 38) / cp.jMainBodyDiv.find(".slider .sliderPage").length;

        // -- builder ---------------------------------------------------------------------

        cp.loadTitles = function () {

            cp.settings.pages = new Array();

            var pageIndex = 0;

            cp.jSlider.find(".sliderPage").each(function(index, item) {
                var jItem = $(item);

                var beforeShow = jItem.data("beforeShow");

                if (beforeShow == "") beforeShow = null;
                if (beforeShow != null) {
                    beforeShow = new Function(beforeShow);
                }

                var title = {
                    id: jItem.prop("id"),
                    index: pageIndex++,
                    titleText: jItem.data("title"),
                    beforeShow: beforeShow,
                    clicable: jItem.data("clicable") == true,
                    height: jItem.data("")
                };

                cp.settings.pages.push(title);
            });
        };

        cp.buildBarHtml = function() {
           
            // load html basico:
            cp.jMainBodyDiv.before(cp.jMainBarDiv);
            cp.jMainBarDiv.addClass("fSlider");
            cp.jMainBarDiv.html("");

            cp.jDarkGray = $(document.createElement("div")).addClass("darkGrayLayer");
            cp.jLightBlue = $(document.createElement("div")).addClass("lightBlueLayer");
            cp.jDarkBlue = $(document.createElement("div")).addClass("darkBlueLayer");
            cp.jWhite = $(document.createElement("div")).addClass("whiteLayer");
            cp.jLightGray = $(document.createElement("div")).addClass("lightGrayLayer");
            cp.jBlack = $(document.createElement("div")).addClass("blackLayer");
            cp.jClicable = $(document.createElement("div")).addClass("clicableLayer");

            cp.jLightGrayLeft = $(document.createElement("div")).addClass("lightGrayLeft");
            cp.jLightGrayCenter = $(document.createElement("div")).addClass("lightGrayCenter");
            cp.jLightGrayRight = $(document.createElement("div")).addClass("lightGrayRight");

            cp.jLightGray.append(cp.jLightGrayLeft);
            cp.jLightGray.append(cp.jLightGrayCenter);
            cp.jLightGray.append(cp.jLightGrayRight);

            cp.jMainBarDiv.append(cp.jDarkGray);
            cp.jMainBarDiv.append(cp.jLightBlue);
            cp.jMainBarDiv.append(cp.jDarkBlue);
            cp.jMainBarDiv.append(cp.jWhite);
            cp.jMainBarDiv.append(cp.jLightGray);
            cp.jMainBarDiv.append(cp.jBlack);
            cp.jMainBarDiv.append(cp.jClicable);

            // load titulos

            if (cp.settings.pages == undefined) cp.loadTitles();

            cp.jWhite.append(cp.createSlicer(4));

            for (var i = 0; i < cp.settings.pages.length; i++) {
                var title = cp.settings.pages[i];

                cp.jWhite.append(cp.createTitle(64 + i * cp.blockWidth, title.titleText));
                cp.jWhite.append(cp.createSlicer(4 + (i + 1) * cp.blockWidth));

                cp.jBlack.append(cp.createTitle(64 + i * cp.blockWidth, title.titleText, true));
            }

            // configure positions and sizes

            cp.jBlack.find(".text:eq(" + cp.settings.index + ")").show();

            cp.jDarkGray.css("left", cp.darkGrayLeftValue() + "px");

            cp.jLightBlue.css("width", cp.settings.width + "px");

            cp.jDarkBlue.css("width", cp.darkBlueWidthValue() + "px");

            cp.jLightGray.css({
                "width": (cp.blockWidth - 12) + "px",
                "left": cp.lightGrayLeftValue() + "px"
            });

            cp.jLightGrayCenter.css("width", (cp.blockWidth - 70) + "px");

            cp.jClicable.css("width", cp.settings.width + "px").click(cp.click).mousemove(cp.clicableHover);

            cp.jSlider.find(".sliderPage").height(1);
            cp.jSlider.find(".sliderPage:eq(" + cp.settings.index + ")").css("height", "");
        };

        cp.buildBodyHtml = function () {

            cp.jMainBodyDiv.css("width", cp.settings.width + "px");
            cp.jSlider.css("width", ((cp.settings.width - 10) * 3) + "px");
            cp.jSlider.find(".sliderPage").css("width", (cp.settings.width - 22) + "px");
        };

        // -- properties methods ----------------------------------------------------------

        cp.darkGrayLeftValue = function() { return 24 + cp.settings.index * cp.blockWidth; };
        cp.darkBlueWidthValue = function() { return 34 + cp.settings.index * cp.blockWidth; };
        cp.lightGrayLeftValue = function() { return 22 + cp.settings.index * cp.blockWidth; };

        // -- methods ---------------------------------------------------------------------

        cp.createSlicer = function(location) {
            //<div class="slicer" style="left: 4px;"></div>
            return $(document.createElement("div"))
                .addClass("slicer")
                .css("left", location + "px");
        };

        cp.createTitle = function(location, text, hide) {
            //<div class="text" style="left: 68px;">Titulo 1</div>
            var title = $(document.createElement("div"))
                .addClass("text")
                .css("left", location + "px")
                .html(text);

            if (hide === true) title.hide();

            return title;
        };

        cp.click = function(e) {
            if (!cp.clickIsEnable(e)) return;

            var indexClick = parseInt(e.offsetX / cp.blockWidth);

            cp.moveToIndex(indexClick);
        };

        cp.clicableHover = function(e) {
            cp.jClicable.css("cursor", cp.clickIsEnable(e) ? "pointer" : "");
        };

        cp.clickIsEnable = function(e) {
            var indexClick = parseInt(e.offsetX / cp.blockWidth);

            var blockOffset = e.offsetX - cp.blockWidth * indexClick;

            // Click was to near of the division
            if (blockOffset < 50 || cp.blockWidth - blockOffset < 10) return false;

            // Clicked the same block
            if (cp.settings.index == indexClick) return false;

            // If enable click is disable ignore the click.
            return cp.settings.pages[indexClick].clicable;
        };

        cp.callBeforeShow = function() {
            try {
                cp.settings.pages[cp.settings.index].beforeShow();
            } catch (e) {

            }
        };

        cp.setHeightsToTransition = function(nextIndex) {
            var mainHeight = cp.jMainBodyDiv.outerHeight();

            cp.jMainBodyDiv.height(mainHeight);

            // Get the height of the current displayed page.
            var displayedPageHeight = 0;

            cp.jSlider.find(".sliderPage").each(function(index, item) {
                var itemHeight = $(item).height();

                if (itemHeight > displayedPageHeight) displayedPageHeight = itemHeight;
            });

            // Get the height of the page that will be displayed.
            var willBeHeight = cp.jSlider.find(".sliderPage:eq(" + nextIndex + ")").css("height", "").height();

            // Calculate the new height of the main body div.
            var newMainHeight = mainHeight - displayedPageHeight + willBeHeight;

            // Define the new heights of the pages.
            cp.jSlider.find(".sliderPage").height(1);
            cp.jSlider.find(".sliderPage:eq(" + nextIndex + ")").css("height", "");

            // Animate to new height.
            cp.jMainBodyDiv.stop().animate({ "height": newMainHeight + "px" }, cp.settings.effectTime / 2);

            // Remove fixed height after animation.
            setTimeout(function() {
                cp.jMainBodyDiv.stop().css("height", "");
            }, cp.settings.effectTime / 2);
        };

        cp.moveForward = function() {
            cp.jBlack.find(".text").hide();

            cp.jDarkGray.stop()
                .animate({ "left": cp.darkGrayLeftValue() + "px" }, cp.settings.effectTime);

            cp.jLightGray.stop()
                .animate({ "left": cp.lightGrayLeftValue() + "px" }, cp.settings.effectTime);

            cp.setHeightsToTransition(cp.settings.index);

            setTimeout(function() {
                cp.jBlack.find(".text:eq(" + cp.settings.index + ")").show();

                cp.jDarkBlue.stop()
                    .animate({ "width": cp.darkBlueWidthValue() + "px" }, cp.settings.effectTime);
                
            }, cp.settings.effectTime / 2);

            cp.jSlider
                .animate({ "margin-left": (-1 * cp.settings.index * (cp.settings.width - 10)) + "px" }, cp.settings.effectTime);
        };

        cp.moveBackward = function() {
            cp.jBlack.find(".text").hide();

            cp.jDarkBlue.stop()
                .animate({ "width": cp.darkBlueWidthValue() + "px" }, cp.settings.effectTime);

            setTimeout(function() {
                cp.jDarkGray.stop()
                    .animate({ "left": cp.darkGrayLeftValue() + "px" }, cp.settings.effectTime);

                cp.jLightGray.stop()
                    .animate({ "left": cp.lightGrayLeftValue() + "px" }, cp.settings.effectTime);

                cp.jSlider
                    .animate({ "margin-left": (-1 * cp.settings.index * (cp.settings.width - 10)) + "px" }, cp.settings.effectTime);

                cp.setHeightsToTransition(cp.settings.index);
            }, cp.settings.effectTime / 2);

            setTimeout(function () {
                cp.jBlack.find(".text:eq(" + cp.settings.index + ")").show();
            }, cp.settings.effectTime * 1.3);
        };

        cp.moveNext = function() {
            if (cp.settings.index + 1 >= cp.settings.pages.length) return;
            cp.settings.index++;
            cp.moveForward();
            cp.callBeforeShow();
        };

        cp.movePrevious = function() {
            if (cp.settings.index - 1 < 0) return;
            cp.settings.index--;
            cp.moveBackward();
            cp.callBeforeShow();
        };

        cp.moveToIndex = function(index) {
            if (index < 0 || cp.settings.pages.length <= index) return;

            var forward = cp.settings.index < index;

            cp.settings.index = index;

            if (forward) cp.moveForward();
            else cp.moveBackward();

            cp.callBeforeShow();
        };

        cp.moveToId = function(id) {
            var page;

            for (var i = 0; i < cp.settings.pages.length; i++) {
                if (cp.settings.pages[i].id == id) {
                    page = cp.settings.pages[i];
                    break;
                }
            }

            if (page != undefined) {
                cp.moveToIndex(page.index);
            }
        };
    }(jQuery));

    // -- constructor ---------------------------------------------------------------------

    cp.buildBarHtml();
    cp.buildBodyHtml();
}