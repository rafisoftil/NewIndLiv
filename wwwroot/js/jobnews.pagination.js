(function (window, $) {
  'use strict';
  if (!$) {
    console.warn('jobnews.pagination: jQuery not found — pagination will not initialize.');
    return;
  }

  var NS = '.jobPagination';

  // Usage: initJobPagination(containerSelector) - containerSelector defaults to 'body'
  window.initJobPagination = function (containerSelector) {
    var $container = $(containerSelector || 'body');
    var $items = $container.find('.job-item');
    var totalItems = $items.length;
    var $pagination = $container.find('#jobPagination');
    var $pageInfo = $container.find('#pageInfo');
    var $itemsPerPageSelect = $container.find('#itemsPerPage');

    // nothing to wire-up
    if ($pagination.length === 0 || $itemsPerPageSelect.length === 0) {
      return { initialized: false, totalItems: totalItems };
    }

    var itemsPerPage = parseInt($itemsPerPageSelect.val(), 10);
    if (isNaN(itemsPerPage) || itemsPerPage <= 0) itemsPerPage = 10;
    var currentPage = 1;

    // Unbind previous namespaced handlers to avoid double-binding when re-initialized
    $pagination.off(NS);
    $itemsPerPageSelect.off(NS);

    function updatePageInfo() {
      var start = totalItems === 0 ? 0 : (currentPage - 1) * itemsPerPage + 1;
      var end = Math.min(currentPage * itemsPerPage, totalItems);
      $pageInfo.text(totalItems === 0 ? 'No items to show' : 'Showing ' + start + ' - ' + end + ' of ' + totalItems);
    }

    function buildPagination() {
      $pagination.empty();
      var totalPages = Math.max(1, Math.ceil(totalItems / itemsPerPage));

      function pageItem(label, page, disabled, active) {
        var cls = 'page-item' + (disabled ? ' disabled' : '') + (active ? ' active' : '');
        return '<li class="' + cls + '"><a class="page-link" href="#" data-page="' + page + '">' + label + '</a></li>';
      }

      $pagination.append(pageItem('Previous', currentPage - 1, currentPage <= 1, false));

      // truncated page list (max 7)
      var maxButtons = 7;
      var startPage = Math.max(1, currentPage - Math.floor(maxButtons / 2));
      var endPage = Math.min(totalPages, startPage + maxButtons - 1);
      if (endPage - startPage < maxButtons - 1) startPage = Math.max(1, endPage - maxButtons + 1);

      if (startPage > 1) {
        $pagination.append(pageItem(1, 1, false, currentPage === 1));
        if (startPage > 2) $pagination.append('<li class="page-item disabled"><span class="page-link">…</span></li>');
      }

      for (var p = startPage; p <= endPage; p++) {
        $pagination.append(pageItem(p, p, false, p === currentPage));
      }

      if (endPage < totalPages) {
        if (endPage < totalPages - 1) $pagination.append('<li class="page-item disabled"><span class="page-link">…</span></li>');
        $pagination.append(pageItem(totalPages, totalPages, false, currentPage === totalPages));
      }

      $pagination.append(pageItem('Next', currentPage + 1, currentPage >= totalPages, false));
    }

    function renderPage(page) {
      var totalPages = Math.max(1, Math.ceil(totalItems / itemsPerPage));
      if (typeof page !== 'number' || page < 1) page = 1;
      if (page > totalPages) page = totalPages;
      currentPage = page;

      var start = (currentPage - 1) * itemsPerPage;
      var end = start + itemsPerPage;

      $items.each(function (idx) {
        $(this).toggle(idx >= start && idx < end);
      });

      updatePageInfo();
      buildPagination();
    }

    // handlers
    $pagination.on('click' + NS, '.page-link', function (e) {
      e.preventDefault();
      var $link = $(this);
      if ($link.closest('.page-item').hasClass('disabled') || $link.closest('.page-item').hasClass('active')) return;
      var page = parseInt($link.data('page'), 10);
      if (isNaN(page)) return;
      renderPage(page);
      // scroll for UX
      var $list = $container.find('.job-listings');
      if ($list.length) $('html, body').animate({ scrollTop: $list.offset().top - 70 }, 200);
    });

    $itemsPerPageSelect.on('change' + NS, function () {
      itemsPerPage = parseInt($(this).val(), 10);
      if (isNaN(itemsPerPage) || itemsPerPage <= 0) itemsPerPage = 10;
      totalItems = $container.find('.job-item').length;
      renderPage(1);
    });

    // expose refresh via container data
    $container.data('jobPagination', {
      refresh: function () {
        $items = $container.find('.job-item');
        totalItems = $items.length;
        renderPage(1);
      }
    });

    renderPage(1);

    return {
      initialized: true,
      totalItems: totalItems,
      refresh: function () {
        $container.data('jobPagination').refresh();
      }
    };
  };
})(window, window.jQuery);