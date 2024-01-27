
(function () {
    'use strict';

    var app = angular.module('app');
    app.run(appRun);
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());

    
    }


    function getStates() {
        return [
            {
                state: "report-viewers",
                config: {
                    url: "report-viewrs",
                    templateUrl: "app/views/report-viewer/report-viewer.html",
                    controller: "reportViwerController as vm",
                    title: "Reports"
                }
            },
            //-------------Module-------
            {

                state: 'modules',
                config: {
                    url: '/modules?ps&pn&q',
                    templateUrl: 'app/views/module/modules.html',
                    controller: 'modulesController as vm',
                    title: 'Module'
                }
            }, {
                state: 'module-create',
                config: {
                    url: '/module-create',
                    templateUrl: 'app/views/module/moduleAdd.html',
                    controller: 'moduleAddController  as vm',
                    title: 'Module'
                }
            }, {
                state: "module-modify",
                config: {
                    url: "/module-modify/:moduleId",
                    templateUrl: 'app/views/module/moduleAdd.html',
                    controller: 'moduleAddController  as vm',
                    title: 'Module'
                }
            },

            //-------------Features-------
            {
                state: 'features',
                config: {
                    url: '/features?ps&pn&q',
                    templateUrl: 'app/views/feature/features.html',
                    controller: 'featuresController as vm',
                    title: 'Feature'
                }
            }, {
                state: 'feature-create',
                config: {
                    url: '/feature-create',
                    templateUrl: 'app/views/feature/featureAdd.html',
                    controller: 'featureAddController as vm',
                    title: 'Feature'
                }
            }, {
                state: "feature-modify",
                config: {
                    url: "/feature-modify/:featureId",
                    templateUrl: 'app/views/feature/featureAdd.html',
                    controller: 'featureAddController as vm',
                    title: 'Feature update'
                }
            },
            //------------------------Role----------------------------------
            {
                state: 'roles',
                config: {
                    url: '/roles?ps&pn&q',
                    templateUrl: 'app/views/role/roles.html',
                    controller: 'RolesController as vm',
                    title: 'Role'
                }
            }, {
                state: 'role-create',
                config: {
                    url: '/role-create',
                    templateUrl: 'app/views/role/roleAdd.html',
                    controller: 'RoleAddController as vm',
                    title: 'Role'
                }
            }, {
                state: "role-modify",
                config: {
                    url: "/role-modify/:roleId",
                    templateUrl: "app/views/role/roleAdd.html",
                    controller: "RoleAddController as vm",
                    title: "Role"
                }
            },
            {
                state: "role-features",
                config: {
                    url: "role-features/:roleId",
                    templateUrl: "app/views/role-feature/roleFeatures.html",
                    controller: "roleFeaturesController as vm",
                    title: "Role"
                }
            },
            //-----------------------User-----------------------------------
            {
                state: 'users',
                config: {
                    url: '/users?ps&pn&q',
                    templateUrl: 'app/views/user/users.html',
                    controller: 'UsersController as vm',
                    title: 'User'
                }
            }, {
                state: 'user-create',
                config: {
                    url: 'user-create',
                    templateUrl: 'app/views/user/userAdd.html',
                    controller: 'UserAddController as vm',
                    title: 'User'
                }
            },
            {
                state: "user-modify",
                config: {
                    url: "/user-modify/:userId",
                    templateUrl: "app/views/user/userAdd.html",
                    controller: "UserAddController as vm",

                    title: "User"
                }
            },
            {
                state: "user-roles",
                config: {
                    url: "/user-roles/:userId",
                    templateUrl: "app/views/user-role/userRoles.html",
                    controller: "UserRolesController as vm",

                    title: "User Role"
                }
            },
            //-------------Rank-------
            {
                state: 'ranks',
                config: {
                    url: '/ranks?ps&pn&q',
                    templateUrl: 'app/views/rank/ranks.html',
                    controller: 'ranksController as vm',
                    title: 'Rank'
                }
            },
            {
                state: 'rank-create',
                config: {
                    url: '/rank-create',
                    templateUrl: 'app/views/rank/rankAdd.html',
                    controller: 'rankAddController as vm',
                    title: 'Rank'
                }
            },
            {
                state: "rank-modify",
                config: {
                    url: '/rank-modify/:id',
                    templateUrl: 'app/views/rank/rankAdd.html',
                    controller: 'rankAddController as vm',
                    title: 'Rank'
                }
            },

            //-------------Rank Category-------
            {
                state: 'rank-categories',
                config: {
                    url: '/rank-categories?ps&pn&q',
                    templateUrl: 'app/views/rank-category/rankCategories.html',
                    controller: 'rankCategoriesController as vm',
                    title: 'Rank Category'
                }
            },
            {
                state: 'rank-category-create',
                config: {
                    url: '/rank-category-create',
                    templateUrl: 'app/views/rank-category/rankCategoryAdd.html',
                    controller: 'rankCategoryAddController as vm',
                    title: 'Rank Category'
                }
            },
            {
                state: "rank-category-modify",
                config: {
                    url: "/rank-category-modify/:id",
                    templateUrl: 'app/views/rank-category/rankCategoryAdd.html',
                    controller: 'rankCategoryAddController as vm',
                    title: 'Rank Category'
                }
            },
            //------------- Category-------
            {
                state: 'categories',
                config: {
                    url: '/categories?ps&pn&q',
                    templateUrl: 'app/views/category/categories.html',
                    controller: 'categoriesController as vm',
                    title: 'Category'
                }
            },
            {
                state: 'category-create',
                config: {
                    url: '/category-create',
                    templateUrl: 'app/views/category/categoryAdd.html',
                    controller: 'categoryAddController as vm',
                    title: 'Category'
                }
            },
            {
                state: "category-modify",
                config: {
                    url: "/category-modify/:id",
                    templateUrl: 'app/views/category/categoryAdd.html',
                    controller: 'categoryAddController as vm',
                    title: 'Category'
                }
            },

            //------------- Sub Category-------
            {
                state: 'sub-categories',
                config: {
                    url: '/sub-categories?ps&pn&q',
                    templateUrl: 'app/views/sub-category/subCategories.html',
                    controller: 'subCategoriesController as vm',
                    title: 'Sub Category'
                }
            },
            {
                state: 'sub-category-create',
                config: {
                    url: '/sub-category-create',
                    templateUrl: 'app/views/sub-category/subCategoryAdd.html',
                    controller: 'subCategoryAddController as vm',
                    title: 'Sub Category'
                }
            },
            {
                state: "sub-category-modify",
                config: {
                    url: "/sub-category-modify/:id",
                    templateUrl: 'app/views/sub-category/subCategoryAdd.html',
                    controller: 'subCategoryAddController as vm',
                    title: 'Sub Category'
                }
            },
            //------------- Commission Type-------
            {
                state: 'commission-types',
                config: {
                    url: '/commission-types?ps&pn&q',
                    templateUrl: 'app/views/commission-type/commissionTypes.html',
                    controller: 'commissionTypesController as vm',
                    title: 'Commission Type'
                }
            },
            {
                state: 'commission-type-create',
                config: {
                    url: '/commission-type-create',
                    templateUrl: 'app/views/commission-type/commissionTypeAdd.html',
                    controller: 'commissionTypeAddController as vm',
                    title: 'Commission Type'
                }
            },
            {
                state: "commission-type-modify",
                config: {
                    url: "/commission-type-modify/:id",
                    templateUrl: 'app/views/commission-type/commissionTypeAdd.html',
                    controller: 'commissionTypeAddController as vm',
                    title: 'Commission Type'
                }
            },

            //------------- Sub Branch-------
            {
                state: 'sub-branches',
                config: {
                    url: '/sub-branches?ps&pn&q',
                    templateUrl: 'app/views/sub-branch/subBranches.html',
                    controller: 'subBranchesController as vm',
                    controllerAs: 'vm',
                    title: 'Sub Branch'
                }
            },
            {
                state: 'sub-branch-create',
                config: {
                    url: '/sub-branch-create',
                    templateUrl: 'app/views/sub-branch/subBranchAdd.html',
                    controller: 'subBranchAddController as vm',
                    title: 'Sub Branch'
                }
            },
            {
                state: "sub-branch-modify",
                config: {
                    url: "/sub-branch-modify/:id",
                    templateUrl: 'app/views/sub-branch/subBranchAdd.html',
                    controller: 'subBranchAddController as vm',
                    title: 'Sub Branch'
                }
            },
            //-------------Officer Stream-------
            {
                state: 'officer-streams',
                config: {
                    url: '/officer-streams?ps&pn&q',
                    templateUrl: 'app/views/officer-stream/officerStreams.html',
                    controller: 'officerStreamsController as vm',
                    title: 'Officer Stream'
                }
            },
            {
                state: 'officer-stream-create',
                config: {
                    url: '/officer-stream-create',
                    templateUrl: 'app/views/officer-stream/officerStreamAdd.html',
                    controller: 'officerStreamAddController as vm',
                    title: 'Officer Stream'
                }
            },
            {
                state: "officer-stream-modify",
                config: {
                    url: "/officer-stream-modify/:id",
                    templateUrl: 'app/views/officer-stream/officerStreamAdd.html',
                    controller: 'officerStreamAddController as vm',
                    title: 'Officer Stream'
                }
            },

            //-------------Batch-------
            {
                state: 'batches',
                config: {
                    url: '/batches?ps&pn&q',
                    templateUrl: 'app/views/batch/batches.html',
                    controller: 'batchesController as vm',
                    title: 'Batch'
                }
            },
            {
                state: 'batch-create',
                config: {
                    url: '/batch-create',
                    templateUrl: 'app/views/batch/batchAdd.html',
                    controller: 'batchAddController as vm',
                    title: 'Batch'
                }
            },
            {
                state: "batch-modify",
                config: {
                    url: "batch-modify/:id",
                    templateUrl: 'app/views/batch/batchAdd.html',
                    controller: 'batchAddController as vm',
                    title: 'Batch'
                }
            },

            //-------------Execution Remarks-------
            {
                state: 'execution-remarks',
                config: {
                    url: '/execution-remarks/:type?ps&pn&q',
                    templateUrl: 'app/views/execution-remark/executionRemarks.html',
                    controller: 'executionRemarksController as vm',
                    title: 'Execution Remarks'
                }
            },
            {
                state: 'execution-remark-create',
                config: {
                    url: '/execution-remark-create/:type',
                    templateUrl: 'app/views/execution-remark/executionRemarkAdd.html',
                    controller: 'executionRemarkAddController as vm',
                    title: 'Execution Remarks'
                }
            },
            {
                state: "execution-remark-modify",
                config: {
                    url: "execution-remark-modify/:type/:id",
                    templateUrl: 'app/views/execution-remark/executionRemarkAdd.html',
                    controller: 'executionRemarkAddController as vm',
                    title: 'Execution Remarks'
                }
            },
            //-------------Country-------
            {
                state: 'countries',
                config: {
                    url: '/countries?ps&pn&q',
                    templateUrl: 'app/views/country/countries.html',
                    controller: 'countriesController as vm',
                    title: 'Country'
                }
            }, {
                state: 'country-create',
                config: {
                    url: '/country-create',
                    templateUrl: 'app/views/country/countryAdd.html',
                    controller: 'countryAddController as vm',
                    title: 'Country'
                }
            }, {
                state: "country-modify",
                config: {
                    url: "/country-modify/:id",
                    templateUrl: 'app/views/country/countryAdd.html',
                    controller: 'countryAddController as vm',
                    title: 'Country'
                }
            },
            //-------------Member Role-------
            {
                state: 'member-roles',
                config: {
                    url: '/member-roles?ps&pn&q',
                    templateUrl: 'app/views/member-role/memberRoles.html',
                    controller: 'memberRolesController as vm',
                    title: 'Member Role'
                }
            }, {
                state: 'member-role-create',
                config: {
                    url: '/member-role-create',
                    templateUrl: 'app/views/member-role/memberRoleAdd.html',
                    controller: 'memberRoleAddController as vm',
                    title: 'Member Role'
                }
            }, {
                state: "member-role-modify",
                config: {
                    url: "/member-role-modify/:id",
                    templateUrl: 'app/views/member-role/memberRoleAdd.html',
                    controller: 'memberRoleAddController as vm',
                    title: 'Member Role'
                }
            },
            //---------MaritalType--------
            {
                state: 'marital-types',
                config: {
                    url: '/marital-types',
                    templateUrl: 'app/views/marital-type/maritalTypes.html',
                    controller: 'maritalTypesController as vm',
                    title: 'Marital Type'
                }
            },
            {
                state: 'marital-type-create',
                config: {
                    url: '/marital-type-create',
                    templateUrl: 'app/views/marital-type/maritalTypeAdd.html',
                    controller: 'maritalTypeAddController as vm',
                    title: 'Marital Type'
                }
            },
            {
                state: 'marital-type-modify',
                config: {
                    url: 'marital-type-modify/:id',
                    templateUrl: 'app/views/marital-type/maritalTypeAdd.html',
                    controller: 'maritalTypeAddController as vm',
                    title: 'Marital Type'
                }
            },
            //---------------- Branch ----------------------

            {
                state: 'branches',
                config: {
                    url: '/branches?ps&pn&q',
                    templateUrl: 'app/views/branch/branches.html',
                    controller: 'branchesController as vm',
                    title: 'Branch'
                }
            },
            {
                state: 'branch-create',
                config: {
                    url: '/branch-create',
                    templateUrl: 'app/views/branch/branchAdd.html',
                    controller: 'branchAddController as vm',
                    title: 'Branch'
                }
            },
            {
                state: "branch-modify",
                config: {
                    url: "branch-modify/:id",
                    templateUrl: 'app/views/branch/branchAdd.html',
                    controller: 'branchAddController as vm',
                    title: 'Branch'
                }
            },
            // ------------------- Subject ------------------------
            {
                state: 'subjects',
                config: {
                    url: '/subjects?ps&pn&q',
                    templateUrl: 'app/views/subject/subjects.html',
                    controller: 'subjectController as vm',
                    title: 'Subject'
                }
            },
            {
                state: 'subject-create',
                config: {
                    url: '/subject-create',
                    templateUrl: 'app/views/subject/subjectAdd.html',
                    controller: 'subjectAddController as vm',
                    title: 'Subject'
                }
            },

            {
                state: "subject-modify",
                config: {
                    url: "/subject-modify/:id",
                    templateUrl: 'app/views/subject/subjectAdd.html',
                    controller: 'subjectAddController as vm',
                    title: 'Subject'
                }
            },
            // ------------------- Religion ------------------------
            {
                state: 'religions',
                config: {
                    url: '/religions?ps&pn&q',
                    templateUrl: 'app/views/religion/religions.html',
                    controller: 'religionsController as vm',
                    title: 'Religion'
                }
            },
            {
                state: 'religion-create',
                config: {
                    url: '/religion-create',
                    templateUrl: 'app/views/religion/religionAdd.html',
                    controller: 'religionAddController as vm',
                    title: 'Religion'
                }
            },

            {
                state: "religion-modify",
                config: {
                    url: "/religion-modify/:id",
                    templateUrl: 'app/views/religion/religionAdd.html',
                    controller: 'religionAddController as vm',
                    title: 'Religion'
                }
            },

            //-------------Employee-------
            {
                state: 'employees',
                config: {
                    url: '/employees?ps&pn&q',
                    templateUrl: 'app/views/employee/employees.html',
                    controller: 'employeesController as vm',
                    title: 'Employee'
                }
            }, {
                state: 'employee-create',
                config: {
                    url: '/employee-create',
                    templateUrl: 'app/views/employee/employeeAdd.html',
                    controller: 'employeeAddController as vm',
                    title: 'Employee'
                }
            }, {
                state: "employee-tabs.employee-modify",
                config: {
                    url: "/employee-modify/:id",
                    templateUrl: 'app/views/employee/employeeAdd.html',
                    controller: 'employeeAddController as vm',
                    title: 'Employee'
                }
            },
            //-------------Officers-------
            {
                state: 'officers',
                config: {
                    url: '/officers?ps&pn&q',
                    templateUrl: 'app/views/officers/officers.html',
                    controller: 'officersController as vm',
                    title: 'Officers'
                }
            },
            //-------------Employee Tab-------
            {
                state: 'employee-tabs',
                config: {
                    url: '/employee-tabs?employeeId',
                    templateUrl: 'app/views/employeeTab/index.html',
                    controller: 'employeeTabController as vm',
                    title: 'Update Officers Detail'
                }
            },


            //-------------Employee General-------
            {
                state: 'employee-tabs.employee-generals',
                config: {
                    url: '/employee-general-detail',
                    templateUrl: 'app/views/employee-general/employeeGenerals.html',
                    controller: 'employeeGeneralsController as vm',
                    title: 'Update Officers Detail'
                }
            }
            ,
            {
                state: 'employee-tabs.employee-general-modify',
                config: {
                    url: '/employee-general-detail/:id',
                    templateUrl: 'app/views/employee-general/employeeGeneralAdd.html',
                    controller: 'employeeGeneralAddController as vm',
                    title: 'Update Officers Detail'
                }
            },
            //-------------Physical Condition-------
            {
                state: 'employee-tabs.physical-conditions',
                config: {
                    url: '/physical-condition-detail',
                    templateUrl: 'app/views/physical-condition/physicalConditions.html',
                    controller: 'physicalConditionsController as vm',
                    title: 'Update Physical Condition'
                }
            }
            ,
            {
                state: 'employee-tabs.physical-condition-modify',
                config: {
                    url: '/physical-condition-detail/:id',
                    templateUrl: 'app/views/physical-condition/physicalConditionAdd.html',
                    controller: 'physicalConditionAddController as vm',
                    title: 'Update Physical Condition'
                }
            },
            //-------------Employee Other-------
            {
                state: 'employee-tabs.employee-others',
                config: {
                    url: '/employee-other-detail',
                    templateUrl: 'app/views/employee-other/employeeOthers.html',
                    controller: 'employeeOthersController as vm',
                    title: 'Update Officers Other Information'
                }
            }
            ,
            {
                state: 'employee-tabs.employee-other-modify',
                config: {
                    url: '/employee-other-detail/:id',
                    templateUrl: 'app/views/employee-other/employeeOtherAdd.html',
                    controller: 'employeeOtherAddController as vm',
                    title: 'Update Officers Other Information'
                }
            },
            //-------------Employee Other Image-------
            {
                state: 'employee-tabs.employee-other-image',
                config: {
                    url: '/employee-other-image/:id',
                    templateUrl: 'app/views/employee-other/employeeOtherImage.html',
                    controller: 'employeeOtherImageController as vm',
                    title: 'EmployeeOther Image'
                }
            },

            //-------------Previous Experience-------
            {
                state: 'employee-tabs.employee-previous-experiences',
                config: {
                    url: '/employee-previous-experience-detail',
                    templateUrl: 'app/views/previous-experience/previousExperience.html',
                    controller: 'previousExperienceController as vm',
                    title: 'Previous Experience'
                }
            }
            ,
            {
                state: 'employee-tabs.employee-previous-experience-modify',
                config: {
                    url: '/employee-previous-experience-detail/:id',
                    templateUrl: 'app/views/previous-experience/previousExperienceAdd.html',
                    controller: 'previousExperienceController as vm',
                    title: 'Previous Experience'
                }
            },

            //-------------Pre Commission Course-------
            {
                state: 'employee-tabs.pre-commission-courses',
                config: {
                    url: '/pre-commission-courses',
                    templateUrl: 'app/views/pre-commission-course/preCommissionCourses.html',
                    controller: 'preCommissionCoursesController as vm',
                    title: 'Pre Commission Course'
                }
            },
            {
                state: 'employee-tabs.pre-commission-course-create',
                config: {
                    url: '/pre-commission-course-create/:id/:preCommissionCourseId',
                    templateUrl: 'app/views/pre-commission-course/preCommissionCourseAdd.html',
                    controller: 'preCommissionCourseAddController as vm',
                    title: 'Pre Commission Course'
                }
            },
            {
                state: 'employee-tabs.pre-commission-course-modify',
                config: {
                    url: '/pre-commission-course-modify/:id/:preCommissionCourseId',
                    templateUrl: 'app/views/pre-commission-course/preCommissionCourseAdd.html',
                    controller: 'preCommissionCourseAddController as vm',
                    title: 'Pre Commission Course'
                }
            },

            //-------------Pre Commission Course Detail-------
            {
                state: 'employee-tabs.pre-commission-course-details',
                config: {
                    url: '/pre-commission-course-details/:preCommissionCourseId',
                    templateUrl: 'app/views/pre-commission-course-detail/preCommissionCourseDetails.html',
                    controller: 'preCommissionCourseDetailsController as vm',
                    title: 'Pre Commission Course Detail'
                }
            },
            {
                state: 'employee-tabs.pre-commission-course-detail-create',
                config: {
                    url: '/pre-commission-course-detail-create/:preCommissionCourseId/:preCommissionCourseDetailId',
                    templateUrl: 'app/views/pre-commission-course-detail/preCommissionCourseDetailAdd.html',
                    controller: 'preCommissionCourseDetailAddController as vm',
                    title: 'Pre Commission Course Detail'
                }
            },
            {
                state: 'employee-tabs.pre-commission-course-detail-modify',
                config: {
                    url: '/pre-commission-course-detail-modify/:preCommissionCourseId/:preCommissionCourseDetailId',
                    templateUrl: 'app/views/pre-commission-course-detail/preCommissionCourseDetailAdd.html',
                    controller: 'preCommissionCourseDetailAddController as vm',
                    title: 'Pre Commission Course Detail'
                }
            },

            //-------------Education-------
            {
                state: 'employee-tabs.employee-educations',
                config: {
                    url: '/employee-educations',
                    templateUrl: 'app/views/education/educations.html',
                    controller: 'educationsController as vm',
                    title: 'Education Information'
                }
            },
            {
                state: 'employee-tabs.employee-education-create',
                config: {
                    url: '/employee-education-create/:id/:educationId',
                    templateUrl: 'app/views/education/educationAdd.html',
                    controller: 'educationAddController as vm',
                    title: 'Education Information'
                }
            },
            {
                state: 'employee-tabs.employee-education-modify',
                config: {
                    url: '/employee-education-modify/:id/:educationId',
                    templateUrl: 'app/views/education/educationAdd.html',
                    controller: 'educationAddController as vm',
                    title: 'Education Information'
                }
            },
            {
                state: 'employee-tabs.employee-education-upload-certificate',
                config: {
                    url: '/employee-education-upload-certificate/:educationId',
                    templateUrl: 'app/views/education/certificateUpload.html',
                    controller: 'certificateUploadController as vm',
                    title: 'Education Information'
                }
            },

            //-------------Address-------
            {
                state: 'employee-tabs.employee-addresses',
                config: {
                    url: '/employee-addresses',
                    templateUrl: 'app/views/address/addresses.html',
                    controller: 'addressesController as vm',
                    title: 'Address'
                }
            },
            {
                state: 'employee-tabs.employee-address-create',
                config: {
                    url: '/employee-address-create/:id/:addressId',
                    templateUrl: 'app/views/address/addressAdd.html',
                    controller: 'addressAddController as vm',
                    title: 'Address'
                }
            },
            {
                state: 'employee-tabs.employee-address-modify',
                config: {
                    url: '/employee-address-modify/:id/:addressId',
                    templateUrl: 'app/views/address/addressAdd.html',
                    controller: 'addressAddController as vm',
                    title: 'Address'
                }
            },
            //-------------Employee Children-------
            {
                state: 'employee-tabs.employee-childrens',
                config: {
                    url: '/employee-childrens',
                    templateUrl: 'app/views/employee-children/employeeChildrens.html',
                    controller: 'employeeChildrensController as vm',
                    title: 'Children'
                }
            },
            {
                state: 'employee-tabs.employee-children-create',
                config: {
                    url: '/employee-children-create/:id/:employeeChildrenId',
                    templateUrl: 'app/views/employee-children/employeeChildrenAdd.html',
                    controller: 'employeeChildrenAddController as vm',
                    title: 'Children'
                }
            },
            {
                state: 'employee-tabs.employee-children-modify',
                config: {
                    url: '/employee-children-modify/:id/:employeeChildrenId',
                    templateUrl: 'app/views/employee-children/employeeChildrenAdd.html',
                    controller: 'employeeChildrenAddController as vm',
                    title: 'Children'
                }
            },

            //-------------Employee Children Image-------
            {
                state: 'employee-tabs.employee-children-image',
                config: {
                    url: '/employee-children-image/:employeeChildrenId',
                    templateUrl: 'app/views/employee-children/childrenImage.html',
                    controller: 'childrenImageController as vm',
                    title: 'Children Image'
                }
            },
            //-------------Social Attribute-------
            {
                state: 'employee-tabs.employee-social-attribute',
                config: {
                    url: '/social-attribute',
                    templateUrl: 'app/views/social-attribute/socialAttribute.html',
                    controller: 'socialAttributeController as vm',
                    title: 'Social Attribute'
                }
            },
            {
                state: 'employee-tabs.employee-social-attribute-modify',
                config: {
                    url: '/employee-social-attribute-modify',
                    templateUrl: 'app/views/social-attribute/socialAttributeAdd.html',
                    controller: 'socialAttributeController as vm',
                    title: 'Social Attribute'
                }
            },
            //-------------Extracurricular Activities-------
            {
                state: 'employee-tabs.employee-extracurriculars',
                config: {
                    url: '/employee-extracurriculars',
                    templateUrl: 'app/views/extracurricular/extracurriculars.html',
                    controller: 'extracurricularsController as vm',
                    title: 'Extracurricular Activities'
                }
            },
            {
                state: 'employee-tabs.employee-extracurricular-create',
                config: {
                    url: '/employee-extracurricular-create/:id/:extracurricularId',
                    templateUrl: 'app/views/extracurricular/extracurricularAdd.html',
                    controller: 'extracurricularAddController as vm',
                    title: 'Extracurricular Activities'
                }
            },
            {
                state: 'employee-tabs.employee-extracurricular-modify',
                config: {
                    url: '/employee-extracurricular-modify/:id/:extracurricularId',
                    templateUrl: 'app/views/extracurricular/extracurricularAdd.html',
                    controller: 'extracurricularAddController as vm',
                    title: 'Extracurricular Activities'
                }
            },

            //-------------Sports----------
            {
                state: 'employee-tabs.employee-sports',
                config: {
                    url: '/employee-sports',
                    templateUrl: 'app/views/employee-sport/employeeSports.html',
                    controller: 'employeeSportsController as vm',
                    title: 'Sports'
                }
            },
            {
                state: 'employee-tabs.employee-sport-create',
                config: {
                    url: '/employee-sport-create/:id/:employeeSportId',
                    templateUrl: 'app/views/employee-sport/employeeSportAdd.html',
                    controller: 'employeeSportAddController as vm',
                    title: 'Sports'
                }
            },
            {
                state: 'employee-tabs.employee-sport-modify',
                config: {
                    url: '/employee-sport-modify/:id/:employeeSportId',
                    templateUrl: 'app/views/employee-sport/employeeSportAdd.html',
                    controller: 'employeeSportAddController as vm',
                    title: 'Sports'
                }
            },


            //-------------Previous Leave----------
            {
                state: 'employee-tabs.previous-leaves',
                config: {
                    url: '/previous-leaves',
                    templateUrl: 'app/views/previous-leave/previousLeaves.html',
                    controller: 'previousLeavesController as vm',
                    title: 'Previous Leave'
                }
            },
            {
                state: 'employee-tabs.previous-leave-create',
                config: {
                    url: '/previous-leave-create/:id/:previousLeaveId',
                    templateUrl: 'app/views/previous-leave/previousLeaveAdd.html',
                    controller: 'previousLeaveAddController as vm',
                    title: 'Previous Leave'
                }
            },
            {
                state: 'employee-tabs.previous-leave-modify',
                config: {
                    url: '/previous-leave-modify/:id/:previousLeaveId',
                    templateUrl: 'app/views/previous-leave/previousLeaveAdd.html',
                    controller: 'previousLeaveAddController as vm',
                    title: 'Previous Leave'
                }
            },


            //-------------Previous Transfer----------
            {
                state: 'employee-tabs.previous-transfers',
                config: {
                    url: '/previous-transfers',
                    templateUrl: 'app/views/previous-transfer/previousTransfers.html',
                    controller: 'previousTransfersController as vm',
                    title: 'Previous Transfer'
                }
            },
            {
                state: 'employee-tabs.previous-transfer-create',
                config: {
                    url: '/previous-transfer-create/:id/:previousTransferId',
                    templateUrl: 'app/views/previous-transfer/previousTransferAdd.html',
                    controller: 'previousTransferAddController as vm',
                    title: 'Previous Transfer'
                }
            },
            {
                state: 'employee-tabs.previous-transfer-modify',
                config: {
                    url: '/previous-transfer-modify/:id/:previousTransferId',
                    templateUrl: 'app/views/previous-transfer/previousTransferAdd.html',
                    controller: 'previousTransferAddController as vm',
                    title: 'Previous Transfer'
                }
            },


            //-------------Previous Punishment----------
            {
                state: 'employee-tabs.previous-punishments',
                config: {
                    url: '/previous-punishments',
                    templateUrl: 'app/views/previous-punishment/previousPunishments.html',
                    controller: 'previousPunishmentsController as vm',
                    title: 'Previous Punishment'
                }
            },
            {
                state: 'employee-tabs.previous-punishment-create',
                config: {
                    url: '/previous-punishment-create/:id/:previousPunishmentId',
                    templateUrl: 'app/views/previous-punishment/previousPunishmentAdd.html',
                    controller: 'previousPunishmentAddController as vm',
                    title: 'Previous Punishment'
                }
            },
            {
                state: 'employee-tabs.previous-punishment-modify',
                config: {
                    url: '/previous-punishment-modify/:id/:previousPunishmentId',
                    templateUrl: 'app/views/previous-punishment/previousPunishmentAdd.html',
                    controller: 'previousPunishmentAddController as vm',
                    title: 'Previous Punishment'
                }
            },


            //-------------Previous Mission----------
            {
                state: 'employee-tabs.previous-missions',
                config: {
                    url: '/previous-missions',
                    templateUrl: 'app/views/previous-mission/previousMissions.html',
                    controller: 'previousMissionsController as vm',
                    title: 'Previous Mission'
                }
            },
            {
                state: 'employee-tabs.previous-mission-create',
                config: {
                    url: '/previous-mission-create/:id/:previousMissionId',
                    templateUrl: 'app/views/previous-mission/previousMissionAdd.html',
                    controller: 'previousMissionAddController as vm',
                    title: 'Previous Mission'
                }
            },
            {
                state: 'employee-tabs.previous-mission-modify',
                config: {
                    url: '/previous-mission-modify/:id/:previousMissionId',
                    templateUrl: 'app/views/previous-mission/previousMissionAdd.html',
                    controller: 'previousMissionAddController as vm',
                    title: 'Previous Mission'
                }
            },


            //-------------Father-----------
            {
                state: 'employee-tabs.employee-father',
                config: {
                    url: '/employee-father/:relationType',
                    templateUrl: 'app/views/parent/parent.html',
                    controller: 'parentController as vm',
                    title: 'Parent'
                }
            },
            {
                state: 'employee-tabs.employee-parents-modify',
                config: {
                    url: '/employee-parents-modify/:relationType',
                    templateUrl: 'app/views/parent/parentAdd.html',
                    controller: 'parentController as vm',
                    title: 'Parent'
                }
            },
            //-------------Mother-----------
            {
                state: 'employee-tabs.employee-mother',
                config: {
                    url: '/employee-mother/:relationType',
                    templateUrl: 'app/views/parent/parent.html',
                    controller: 'parentController as vm',
                    title: 'Parent'
                }
            },

            //-------------  Step Father-----------
            {
                state: 'employee-tabs.employee-stepfather',
                config: {
                    url: '/employee-stepfather/:relationType',
                    templateUrl: 'app/views/parent/parent.html',
                    controller: 'parentController as vm',
                    title: 'Parent'
                }
            },

            //-------------  Step Mother-----------
            {
                state: 'employee-tabs.employee-stepmother',
                config: {
                    url: '/employee-stepmother/:relationType',
                    templateUrl: 'app/views/parent/parent.html',
                    controller: 'parentController as vm',
                    title: 'Parent'
                }
            },
            //-------------Parent Image-----------
            {
                state: 'employee-tabs.employee-parent-image',
                config: {
                    url: '/employee-parent-image/:relationType',
                    templateUrl: 'app/views/parent/parentImage.html',
                    controller: 'parentImageController as vm',
                    title: 'Parent Image'
                }
            },

            //-------------Sibling-------
            {
                state: 'employee-tabs.employee-siblings',
                config: {
                    url: '/employee-siblings',
                    templateUrl: 'app/views/sibling/siblings.html',
                    controller: 'siblingsController as vm',
                    title: 'Siblings Information'
                }
            },
            {
                state: 'employee-tabs.employee-sibling-create',
                config: {
                    url: '/employee-sibling-create/:id/:siblingId',
                    templateUrl: 'app/views/sibling/siblingAdd.html',
                    controller: 'siblingAddController as vm',
                    title: 'Siblings Information'
                }
            },
            {
                state: 'employee-tabs.employee-sibling-modify',
                config: {
                    url: '/employee-sibling-modify/:id/:siblingId',
                    templateUrl: 'app/views/sibling/siblingAdd.html',
                    controller: 'siblingAddController as vm',
                    title: 'Siblings Information'
                }
            },
            //-------------Sibling Image-------
            {
                state: 'employee-tabs.employee-sibling-image',
                config: {
                    url: '/employee-sibling-image/:siblingId',
                    templateUrl: 'app/views/sibling/siblingImage.html',
                    controller: 'siblingImageController as vm',
                    title: 'Siblings Image'
                }
            },
            //-------------Spouse-------
            {
                state: 'employee-tabs.employee-spouses',
                config: {
                    url: '/employee-spouses',
                    templateUrl: 'app/views/spouse/spouses.html',
                    controller: 'spousesController as vm',
                    title: 'Spouse Information'
                }
            },
            {
                state: 'employee-tabs.employee-spouse-create',
                config: {
                    url: '/employee-spouse-create/:id/:spouseId',
                    templateUrl: 'app/views/spouse/spouseAdd.html',
                    controller: 'spouseAddController as vm',
                    title: 'Spouse Information'
                }
            },
            {
                state: 'employee-tabs.employee-spouse-modify',
                config: {
                    url: '/employee-spouse-modify/:id/:spouseId',
                    templateUrl: 'app/views/spouse/spouseAdd.html',
                    controller: 'spouseAddController as vm',
                    title: 'Spouse Information'
                }
            },
            //-------------Spouse Image-------
            {
                state: 'employee-tabs.employee-spouse-image',
                config: {
                    url: '/employee-spouse-image/:spouseId',
                    templateUrl: 'app/views/spouse/spouseImage.html',
                    controller: 'spouseImageController as vm',
                    title: 'Spouse Image'
                }
            },

            //-------------Spouse Foreign Visit-------
            {
                state: 'employee-tabs.employee-spouse-foreign-visits',
                config: {
                    url: '/employee-spouse-foreign-visits/:spouseId',
                    templateUrl: 'app/views/spouse-foreign-visit/spouseForeignVisits.html',
                    controller: 'spouseForeignVisitsController as vm',
                    title: 'Spouse Foreign Visit'
                }
            },
            {
                state: 'employee-tabs.employee-spouse-foreign-visit-create',
                config: {
                    url: '/employee-spouse-foreign-visit-create/:spouseId/:spouseForeignVisitId',
                    templateUrl: 'app/views/spouse-foreign-visit/spouseForeignVisitAdd.html',
                    controller: 'spouseForeignVisitAddController as vm',
                    title: 'Spouse Foreign Visit'
                }
            },
            {
                state: 'employee-tabs.employee-spouse-foreign-visit-modify',
                config: {
                    url: '/employee-spouse-foreign-visit-modify/:spouseId/:spouseForeignVisitId',
                    templateUrl: 'app/views/spouse-foreign-visit/spouseForeignVisitAdd.html',
                    controller: 'spouseForeignVisitAddController as vm',
                    title: 'Spouse Foreign Visit'
                }
            },


            //-------------Heir / Next of Kin Info-------
            {
                state: 'employee-tabs.employee-heir-next-of-kin-info-list',
                config: {
                    url: '/employee-employee-heir-next-of-kin-info-list',
                    templateUrl: 'app/views/heir-next-of-kin-info/heirNextOfKinInfoList.html',
                    controller: 'heirNextOfKinInfoListController as vm',
                    title: 'Heir & Next of Kin Info'
                }
            },
            {
                state: 'employee-tabs.employee-heir-next-of-kin-info-create',
                config: {
                    url: '/employee-employee-heir-next-of-kin-info-create/:id/:heirNextOfKinInfoId',
                    templateUrl: 'app/views/heir-next-of-kin-info/heirNextOfKinInfoAdd.html',
                    controller: 'heirNextOfKinInfoAddController as vm',
                    title: 'Heir & Next of Kin Info'
                }
            },
            {
                state: 'employee-tabs.employee-heir-next-of-kin-info-modify',
                config: {
                    url: '/employee-employee-heir-next-of-kin-info-modify/:id/:heirNextOfKinInfoId',
                    templateUrl: 'app/views/heir-next-of-kin-info/heirNextOfKinInfoAdd.html',
                    controller: 'heirNextOfKinInfoAddController as vm',
                    title: 'Heir & Next of Kin Info'
                }
            },
            //-------------Heir / Next of Kin Image-----------
            {
                state: 'employee-tabs.employee-heir-next-of-kin-image',
                config: {
                    url: '/employee-heir-next-of-kin-image/:heirNextOfKinInfoId',
                    templateUrl: 'app/views/heir-next-of-kin-info/heirNextOfKinImage.html',
                    controller: 'heirNextOfKinImageController as vm',
                    title: 'Heir & Next of Kin Image'
                }
            },

            //-------------Image-----------
            {
                state: 'employee-tabs.employee-image',
                config: {
                    url: '/employee-image/:type',
                    templateUrl: 'app/views/photo/upload-picture.html',
                    controller: 'pictureUploadController as vm',
                    title: 'Image'
                }
            },
            //{
            //    state: 'employee-tabs.employee-photo-modify',
            //    config: {
            //        url: '/employee-photo-modify/:type',
            //        templateUrl: 'app/views/photo/uploadPhoto.html',
            //        controller: 'uploadPhotoController as vm',
            //        title: 'Image'
            //    }
            //},
            //-------------Signature-----------
            {
                state: 'employee-tabs.employee-signature',
                config: {
                    url: '/employee-signature/:type',
                    templateUrl: 'app/views/photo/upload-signature.html',
                    controller: 'signatureUploadController as vm',
                    title: 'Signature'
                }
            },
            //-------------Employee Dollar Sign-------
            {
                state: 'employee-dollar-signs',
                config: {
                    url: '/employee-dollar-signs?ps&pn&q',
                    templateUrl: 'app/views/employee-dollar-sign/employeeDollarSigns.html',
                    controller: 'employeeDollarSignsController as vm',
                    title: 'Employee Dollar Sign'
                }
            },

            {
                state: 'employee-dollar-sign-create',
                config: {
                    url: '/employee-dollar-sign-create',
                    templateUrl: 'app/views/employee-dollar-sign/employeeDollarSignAdd.html',
                    controller: 'employeeDollarSignAddController as vm',
                    title: 'Employee Dollar Sign'
                }
            },
            {
                state: "employee-dollar-sign-modify",
                config: {
                    url: "employee-dollar-sign-modify/:employeeId",
                    templateUrl: 'app/views/employee-dollar-sign/employeeDollarSignAdd.html',
                    controller: 'employeeDollarSignAddController as vm',
                    title: 'Employee Dollar Sign'
                }
            },

            //-------------Division-------
            {
                state: 'divisions',
                config: {
                    url: '/divisions?ps&pn&q',
                    templateUrl: 'app/views/division/divisions.html',
                    controller: 'divisionsController as vm',
                    title: 'Division'
                }
            },

            {
                state: 'division-create',
                config: {
                    url: '/division-create',
                    templateUrl: 'app/views/division/divisionAdd.html',
                    controller: 'divisionAddController as vm',
                    title: 'Division'
                }
            },
            {
                state: "division-modify",
                config: {
                    url: "division-modify/:id",
                    templateUrl: 'app/views/division/divisionAdd.html',
                    controller: 'divisionAddController as vm',
                    title: 'Division'
                }
            },

            //-------------Result Grade-------
            {
                state: 'result-grades',
                config: {
                    url: '/result-grades?ps&pn&q',
                    templateUrl: 'app/views/result-grade/resultGrades.html',
                    controller: 'resultGradesController as vm',
                    title: 'Result Grade'
                }
            },

            {
                state: 'result-grade-create',
                config: {
                    url: '/result-grade-create',
                    templateUrl: 'app/views/result-grade/resultGradeAdd.html',
                    controller: 'resultGradeAddController as vm',
                    title: 'Result Grade'
                }
            },
            {
                state: "result-grade-modify",
                config: {
                    url: "result-grade-modify/:id",
                    templateUrl: 'app/views/result-grade/resultGradeAdd.html',
                    controller: 'resultGradeAddController as vm',
                    title: 'Result Grade'
                }
            },

            //-------------Sports-------
            {
                state: 'sports',
                config: {
                    url: '/sports?ps&pn&q',
                    templateUrl: 'app/views/sport/sports.html',
                    controller: 'sportsController as vm',
                    title: 'Sports'
                }
            },

            {
                state: 'sport-create',
                config: {
                    url: '/sport-create',
                    templateUrl: 'app/views/sport/sportAdd.html',
                    controller: 'sportAddController as vm',
                    title: 'Sports'
                }
            },
            {
                state: "sport-modify",
                config: {
                    url: "sport-modify/:id",
                    templateUrl: 'app/views/sport/sportAdd.html',
                    controller: 'sportAddController as vm',
                    title: 'Sports'
                }
            },
            //--------------Purpose-----------------


            {
                state: 'purposes',
                config: {
                    url: '/purposes?ps&pn&q',
                    templateUrl: 'app/views/leave-purpose/purpose.html',
                    controller: 'purposeController as vm',
                    title: 'Purpose'
                }
            },
            {
                state: 'purpose-create',
                config: {
                    url: '/purpose-create',
                    templateUrl: 'app/views/leave-purpose/purposeAdd.html',
                    controller: 'purposeAddController as vm',
                    title: 'Purpose'
                }
            },
            {
                state: "purpose-modify",
                config: {
                    url: "purpose-modify/:id",
                    templateUrl: 'app/views/leave-purpose/purposeAdd.html',
                    controller: 'purposeAddController as vm',
                    title: 'Purpose'
                }
            },

            //--------------Color-----------------


            {
                state: 'colors',
                config: {
                    url: '/colors?ps&pn&q',
                    templateUrl: 'app/views/color/colors.html',
                    controller: 'colorsController as vm',
                    title: 'Color'
                }
            },
            {
                state: 'color-create',
                config: {
                    url: '/color-create',
                    templateUrl: 'app/views/color/colorAdd.html',
                    controller: 'colorAddController as vm',
                    title: 'Color'
                }
            },
            {
                state: "color-modify",
                config: {
                    url: "color-modify/:id",
                    templateUrl: 'app/views/color/colorAdd.html',
                    controller: 'colorAddController as vm',
                    title: 'Color'
                }
            },

            //------------- District-------
            {
                state: 'districts',
                config: {
                    url: '/districts?ps&pn&q',
                    templateUrl: 'app/views/district/districts.html',
                    controller: 'districtsController as vm',
                    title: 'Districts'
                }
            },
            {
                state: 'district-create',
                config: {
                    url: '/district-create',
                    templateUrl: 'app/views/district/districtAdd.html',
                    controller: 'districtAddController as vm',
                    title: 'Districts'
                }
            },
            {
                state: "district-modify",
                config: {
                    url: "/district-modify/:id",
                    templateUrl: 'app/views/district/districtAdd.html',
                    controller: 'districtAddController as vm',
                    title: 'Districts'
                }
            },



            //------------- Blood Group-------
            {
                state: 'blood-groups',
                config: {
                    url: '/blood-groups?ps&pn&q',
                    templateUrl: 'app/views/blood-group/bloodGroups.html',
                    controller: 'bloodGroupsController as vm',
                    title: 'Blood Group'
                }
            },
            {
                state: 'blood-group-create',
                config: {
                    url: '/blood-group-create',
                    templateUrl: 'app/views/blood-group/bloodGroupAdd.html',
                    controller: 'bloodGroupAddController as vm',
                    title: 'Blood Group'
                }
            },
            {
                state: "blood-group-modify",
                config: {
                    url: "/blood-group-modify/:id",
                    templateUrl: 'app/views/blood-group/bloodGroupAdd.html',
                    controller: 'bloodGroupAddController as vm',
                    title: 'Blood Group'
                }
            },

            //------------- Eye Vision-------
            {
                state: 'eye-visions',
                config: {
                    url: '/eye-visions?ps&pn&q',
                    templateUrl: 'app/views/eye-vision/eyeVisions.html',
                    controller: 'eyeVisionsController as vm',
                    title: 'Eye Vision'
                }
            },
            {
                state: 'eye-vision-create',
                config: {
                    url: '/eye-vision-create',
                    templateUrl: 'app/views/eye-vision/eyeVisionAdd.html',
                    controller: 'eyeVisionAddController as vm',
                    title: 'Eye Vision'
                }
            },
            {
                state: "eye-vision-modify",
                config: {
                    url: "/eye-vision-modify/:id",
                    templateUrl: 'app/views/eye-vision/eyeVisionAdd.html',
                    controller: 'eyeVisionAddController as vm',
                    title: 'Eye Vision'
                }
            },

            //------------- Medical Category-------
            {
                state: 'medical-categories',
                config: {
                    url: 'medical-categories?ps&pn&q',
                    templateUrl: 'app/views/medical-category/medicalCategories.html',
                    controller: 'medicalCategoriesController as vm',
                    title: 'Medical Category'
                }
            },
            {
                state: 'medical-category-create',
                config: {
                    url: '/medical-category-create',
                    templateUrl: 'app/views/medical-category/medicalCategoryAdd.html',
                    controller: 'medicalCategoryAddController as vm',
                    title: 'Medical Category'
                }
            },
            {
                state: "medical-category-modify",
                config: {
                    url: "/medical-category-modify/:id",
                    templateUrl: 'app/views/medical-category/medicalCategoryAdd.html',
                    controller: 'medicalCategoryAddController as vm',
                    title: 'Medical Category'
                }
            },

            //------------- Physical Structure-------
            {
                state: 'physical-structures',
                config: {
                    url: '/physical-structures?ps&pn&q',
                    templateUrl: 'app/views/physical-structure/physicalStructures.html',
                    controller: 'physicalStructuresController as vm',
                    title: 'Physical Structure'
                }
            },
            {
                state: 'physical-structure-create',
                config: {
                    url: 'physical-structure-create',
                    templateUrl: 'app/views/physical-structure/physicalStructureAdd.html',
                    controller: 'physicalStructureAddController as vm',
                    title: 'Physical Structure'
                }
            },
            {
                state: "physical-structure-modify",
                config: {
                    url: "/physical-structure-modify/:id",
                    templateUrl: 'app/views/physical-structure/physicalStructureAdd.html',
                    controller: 'physicalStructureAddController as vm',
                    title: 'Physical Structure'
                }
            },
            //------------- Leave Type -------
            {
                state: 'leave-type',
                config: {
                    url: '/leave-types?ps&pn&q',
                    templateUrl: 'app/views/leave-Type/leaveTypes.html',
                    controller: 'leaveTypesController as vm',
                    title: 'Leave-Type'
                }
            },
            {
                state: 'leave-type-create',
                config: {
                    url: '/leave-type-create',
                    templateUrl: 'app/views/leave-Type/leaveTypeAdd.html',
                    controller: 'leaveTypeAddController as vm',
                    title: 'Leave-Type'
                }
            },
            {
                state: "leave-type-modify",
                config: {
                    url: "leave-types-modify/:id",
                    templateUrl: 'app/views/leave-Type/leaveTypeAdd.html',
                    controller: 'leaveTypeAddController as vm',
                    title: 'Leave-Type'
                }
            },
            //------------- Leave Policy -------
            {
                state: 'leavePolicyies',
                config: {
                    url: '/leavePolicyies?ps&pn&q',
                    templateUrl: 'app/views/leave-policy/leavePolicyies.html',
                    controller: 'leavePolicyiesController as vm',
                    title: 'Leave Policy'
                }
            },
            {
                state: 'leavePolicy-create',
                config: {
                    url: '/leavePolicyies-create',
                    templateUrl: 'app/views/leave-policy/leavePolicyAdd.html',
                    controller: 'leavePolicyAddController as vm',
                    title: 'Leave Policy'
                }
            },
            {
                state: "leavePolicy-modify",
                config: {
                    url: "leavePolicyies-modify/:id",
                    templateUrl: 'app/views/leave-policy/leavePolicyAdd.html',
                    controller: 'leavePolicyAddController as vm',
                    title: 'Leave Policy'
                }
            },

            //------------- Upazila -------
            {
                state: 'upazilas',
                config: {
                    url: '/upazilas?ps&pn&q',
                    templateUrl: 'app/views/upazila/upazilas.html',
                    controller: 'upazilasController as vm',
                    title: 'Upazila'
                }
            },
            {
                state: 'upazila-create',
                config: {
                    url: '/upazila-create',
                    templateUrl: 'app/views/upazila/upazilaAdd.html',
                    controller: 'upazilaAddController as vm',
                    title: 'Upazila'
                }
            },
            {
                state: "upazila-modify",
                config: {
                    url: "upazila-modify/:id",
                    templateUrl: 'app/views/upazila/upazilaAdd.html',
                    controller: 'upazilaAddController as vm',
                    title: 'Upazila'
                }
            },
            //-------------Exam Category-------
            {
                state: 'exam-categories',
                config: {
                    url: '/exam-categories?ps&pn&q',
                    templateUrl: 'app/views/exam-category/examCategories.html',
                    controller: 'examCategoriesController as vm',
                    title: 'Exam Category'
                }
            },
            {
                state: 'exam-category-create',
                config: {
                    url: '/exam-category-create',
                    templateUrl: 'app/views/exam-category/examCategoryAdd.html',
                    controller: 'examCategoryAddController as vm',
                    title: 'Exam Category'
                }
            },
            {
                state: "exam-category-modify",
                config: {
                    url: '/exam-category-modify/:id',
                    templateUrl: 'app/views/exam-category/examCategoryAdd.html',
                    controller: 'examCategoryAddController as vm',
                    title: 'Exam Category'
                }
            },
            //-------------Examination-------
            {
                state: 'examinations',
                config: {
                    url: '/examinations?ps&pn&q',
                    templateUrl: 'app/views/examination/examinations.html',
                    controller: 'examinationsController as vm',
                    title: 'Examination'
                }
            },
            {
                state: 'examination-create',
                config: {
                    url: '/examination-create',
                    templateUrl: 'app/views/examination/examinationAdd.html',
                    controller: 'examinationAddController as vm',
                    title: 'Examination'
                }
            },
            {
                state: "examination-modify",
                config: {
                    url: '/examination-modify/:id',
                    templateUrl: 'app/views/examination/examinationAdd.html',
                    controller: 'examinationAddController as vm',
                    title: 'Examination'
                }
            },
            //------------- Termination Type-------
            {
                state: 'termination-types',
                config: {
                    url: '/termination-types?ps&pn&q',
                    templateUrl: 'app/views/termination-type/terminationTypes.html',
                    controller: 'terminationTypesController as vm',
                    title: 'Termination Type'
                }
            },
            {
                state: 'termination-type-create',
                config: {
                    url: '/termination-type-create',
                    templateUrl: 'app/views/termination-type/terminationTypeAdd.html',
                    controller: 'terminationTypeAddController as vm',
                    title: 'Termination Type'
                }
            },
            {
                state: "termination-type-modify",
                config: {
                    url: "/termination-type-modify/:id",
                    templateUrl: 'app/views/termination-type/terminationTypeAdd.html',
                    controller: 'terminationTypeAddController as vm',
                    title: 'Termination Type'
                }
            },


            //------------- Age Service Policy-------
            {
                state: 'age-service-policies',
                config: {
                    url: '/age-service-policies?ps&pn&q',
                    templateUrl: 'app/views/age-service-policy/ageServicePolicies.html',
                    controller: 'ageServicePoliciesController as vm',
                    title: 'Age Service Policy'
                }
            },
            {
                state: 'age-service-policy-create',
                config: {
                    url: '/age-service-policy-create',
                    templateUrl: 'app/views/age-service-policy/ageServicePolicyAdd.html',
                    controller: 'ageServicePolicyAddController as vm',
                    title: 'Age Service Policy'
                }
            },
            {
                state: "age-service-policy-modify",
                config: {
                    url: "/age-service-policy-modify/:id",
                    templateUrl: 'app/views/age-service-policy/ageServicePolicyAdd.html',
                    controller: 'ageServicePolicyAddController as vm',
                    title: 'Age Service Policy'
                }
            },
            //-------------Institute Type-------
            {
                state: 'institute-types',
                config: {
                    url: '/institute-types',
                    templateUrl: 'app/views/institute-type/instituteTypes.html',
                    controller: 'instituteTypesController as vm',
                    title: 'Institute Type'
                }
            },
            {
                state: 'institute-type-create',
                config: {
                    url: '/institute-type-create',
                    templateUrl: 'app/views/institute-type/instituteTypeAdd.html',
                    controller: 'instituteTypeAddController as vm',
                    title: 'Institute Type'
                }
            },
            {
                state: "institute-type-modify",
                config: {
                    url: 'institute-type-modify/:id',
                    templateUrl: 'app/views/institute-type/instituteTypeAdd.html',
                    controller: 'instituteTypeAddController as vm',
                    title: 'Institute Type'
                }
            },
            //-------------Institute-------
            {
                state: 'institutes',
                config: {
                    url: '/institutes?ps&pn&q',
                    templateUrl: 'app/views/institute/institutes.html',
                    controller: 'institutesController as vm',
                    title: 'Institute'
                }
            },
            {
                state: 'institute-create',
                config: {
                    url: '/institute-create',
                    templateUrl: 'app/views/institute/instituteAdd.html',
                    controller: 'instituteAddController as vm',
                    title: 'Institute'
                }
            },
            {
                state: "institute-modify",
                config: {
                    url: 'institute-modify/:id',
                    templateUrl: 'app/views/institute/instituteAdd.html',
                    controller: 'instituteAddController as vm',
                    title: 'Institute'
                }
            },
            //-------------Retired Age-------
            {
                state: 'retired-ages',
                config: {
                    url: '/retired-ages?ps&pn&q',
                    templateUrl: 'app/views/retired-age/retiredAges.html',
                    controller: 'retiredAgesController as vm',
                    title: 'Retired Age'
                }
            },
            {
                state: 'retired-age-create',
                config: {
                    url: 'retired-age-create',
                    templateUrl: 'app/views/retired-age/retiredAgeAdd.html',
                    controller: 'retiredAgeAddController as vm',
                    title: 'Retired Age'
                }
            },
            {
                state: "retired-age-modify",
                config: {
                    url: '/retired-age-modify/:id',
                    templateUrl: 'app/views/retired-age/retiredAgeAdd.html',
                    controller: 'retiredAgeAddController as vm',
                    title: 'Retired Age'
                }
            },

            //-------------Employee Service Extension -------
            {
                state: 'employee-service-extensions',
                config: {
                    url: '/employee-service-extensions?ps&pn&q',
                    templateUrl: 'app/views/employee-service-extension/employeeServiceExtensions.html',
                    controller: 'employeeServiceExtensionsController as vm',
                    title: 'Employee Service Extension'
                }
            },
            {
                state: 'employee-service-extension-create',
                config: {
                    url: 'employee-service-extension-create',
                    templateUrl: 'app/views/employee-service-extension/employeeServiceExtensionAdd.html',
                    controller: 'employeeServiceExtensionAddController as vm',
                    title: 'Employee Service Extension'
                }
            },
            {
                state: "employee-service-extension-modify",
                config: {
                    url: '/employee-service-extension-modify/:id',
                    templateUrl: 'app/views/employee-service-extension/employeeServiceExtensionAdd.html',
                    controller: 'employeeServiceExtensionAddController as vm',
                    title: 'Employee Service Extension'
                }
            },
            //-------------Employee Leave-------

            {
                state: 'employeeLeaves',
                config: {
                    url: '/employeeLeaves?ps&pn&q&le',
                    templateUrl: 'app/views/employee-leave/employeeLeaves.html',
                    controller: 'employeeLeavesController as vm',
                    title: 'Employee Leave'
                }
            },
            {
                state: 'current-status-tab.employeeLeave-create',
                config: {
                    url: '/employeeLeave-create',
                    templateUrl: 'app/views/employee-leave/employeeLeaveAdd.html',
                    controller: 'employeeLeaveAddController as vm',
                    title: 'Employee Leave'
                }
            },
            {
                state: "employeeLeave-modify",
                config: {
                    url: "/employeeLeave-modify/:id",
                    templateUrl: 'app/views/employee-leave/employeeLeaveAdd.html',
                    controller: 'employeeLeaveAddController as vm',
                    title: 'Employee Leave'
                }
            },
            {
                state: 'employee-leave-inbox',
                config: {
                    url: '/employee-leave-inbox?ps&pn&q&le',
                    templateUrl: 'app/views/employee-leave/employeeLeaveInbox.html',
                    controller: 'employeeLeaveInboxController as vm',
                    title: 'Employee Leave Inbox'
                }
            },
            {
                state: 'employee-leave-details',
                config: {
                    url: '/employee-leave-details?empLeaveId',
                    templateUrl: 'app/views/employee-leave/IndividualEmployeeLeave.html',
                    controller: 'IndividualEmployeeLeaveController as vm',
                    title: 'Employee Leave Details'
                }
            },
            {
                state: 'ns-dashboard',
                config: {
                    url: '/ns-dashboard?ps&pn&q&le',
                    templateUrl: 'app/views/employee-leave/nsOfficeDashBoard.html',
                    controller: 'nsOfficeDashBoardController as vm',
                    title: 'NS Dashboard'
                }
            },
            {
                state: 'ns-leave-dashboard',
                config: {
                    url: '/employee-leave-dashboard?ps&pn&q&le',
                    templateUrl: 'app/views/employee-leave/nsOfficeLeavedashboard.html',
                    controller: 'nsOfficeLeavedashboardController as vm',
                    title: 'Employee Leave Inbox'
                }
            },
            {
                state: 'employee-leave-status',
                config: {
                    url: '/employee-leave-status',
                    templateUrl: 'app/views/employee-leave/CurrentEmployeeInformation.html',
                    controller: 'CurrentEmployeeInformationController as vm',
                    title: 'employee-leave-status'
                }
            },
            {
                state: 'leave-tracking',
                config: {
                    url: '/leave-tracking?id',
                    templateUrl: 'app/views/employee-leave/leaveTracking.html',
                    controller: 'leaveTrackingController as vm',
                    title: 'Employee Leave Tracking'
                }
            },
            {
                state: 'employee-approved-leaves',
                config: {
                    url: '/employee-approved-leaves?ps&pn&q&le',
                    templateUrl: 'app/views/employee-leave/employeeLeaveApproved.html',
                    controller: 'employeeLeaveApprovedController as vm',
                    title: 'Employee Leave Inbox'
                }
            },

            //-------------Board-------
            {
                state: 'boards',
                config: {
                    url: '/boards?ps&pn&q',
                    templateUrl: 'app/views/board/boards.html',
                    controller: 'boardsController as vm',
                    title: 'Board'
                }
            },
            {
                state: 'board-create',
                config: {
                    url: '/board-create',
                    templateUrl: 'app/views/board/boardAdd.html',
                    controller: 'boardAddController as vm',
                    title: 'Board'
                }
            },
            {
                state: "board-modify",
                config: {
                    url: "board-modify/:id",
                    templateUrl: 'app/views/board/boardAdd.html',
                    controller: 'boardAddController as vm',
                    title: 'Board'
                }
            },

            //-------------Course Category-------
            {
                state: 'course-categories',
                config: {
                    url: '/course-categories?ps&pn&q',
                    templateUrl: 'app/views/course-category/courseCategories.html',
                    controller: 'courseCategoriesController as vm',
                    title: 'Course Category'
                }
            },
            {
                state: 'course-category-create',
                config: {
                    url: '/course-category-create',
                    templateUrl: 'app/views/course-category/courseCategoryAdd.html',
                    controller: 'courseCategoryAddController as vm',
                    title: 'Course Category'
                }
            },
            {
                state: "course-category-modify",
                config: {
                    url: "course-category-modify/:id",
                    templateUrl: 'app/views/course-category/courseCategoryAdd.html',
                    controller: 'courseCategoryAddController as vm',
                    title: 'Course Category'
                }
            },

            //-------------Course Sub Category-------
            {
                state: 'course-sub-categories',
                config: {
                    url: '/course-sub-categories?ps&pn&q',
                    templateUrl: 'app/views/course-sub-category/courseSubCategories.html',
                    controller: 'courseSubCategoriesController as vm',
                    title: 'Course Sub Category'
                }
            },
            {
                state: 'course-sub-category-create',
                config: {
                    url: '/course-sub-category-create',
                    templateUrl: 'app/views/course-sub-category/courseSubCategoryAdd.html',
                    controller: 'courseSubCategoryAddController as vm',
                    title: 'Course Sub Category'
                }
            },
            {
                state: "course-sub-category-modify",
                config: {
                    url: "course-sub-category-modify/:id",
                    templateUrl: 'app/views/course-sub-category/courseSubCategoryAdd.html',
                    controller: 'courseSubCategoryAddController as vm',
                    title: 'Course Sub Category'
                }
            },

            //-------------Employee Run Missing-------
            {
                state: 'employee-run-missings',
                config: {
                    url: '/employee-run-missings?ps&pn&q',
                    templateUrl: 'app/views/employee-run-missing/empRunMissings.html',
                    controller: 'empRunMissingsController as vm',
                    title: 'Employee Run Missing'
                }
            },
            {
                state: 'employee-run-missing-create',
                config: {
                    url: '/employee-run-missing-create',
                    templateUrl: 'app/views/employee-run-missing/empRunMissingAdd.html',
                    controller: 'empRunMissingAddController as vm',
                    title: 'Employee Run Missing'
                }
            },
            {
                state: "employee-run-missing-modify",
                config: {
                    url: "employee-run-missing-modify/:id",
                    templateUrl: 'app/views/employee-run-missing/empRunMissingAdd.html',
                    controller: 'empRunMissingAddController as vm',
                    title: 'Employee Run Missing'
                }
            },

            //-------------Employee Back to Unit-------
            {
                state: 'emp-back-to-units',
                config: {
                    url: '/emp-back-to-units?ps&pn&q',
                    templateUrl: 'app/views/emp-back-to-unit/empBackToUnits.html',
                    controller: 'empBackToUnitsController as vm',
                    title: 'Employee Back to Unit'
                }
            },
            {
                state: 'emp-back-to-unit-create',
                config: {
                    url: '/emp-back-to-unit-create',
                    templateUrl: 'app/views/emp-back-to-unit/empBackToUnitAdd.html',
                    controller: 'empBackToUnitAddController as vm',
                    title: 'Employee Back to Unit'
                }
            },
            {
                state: "emp-back-to-unit-modify",
                config: {
                    url: "emp-back-to-unit-modify/:id",
                    templateUrl: 'app/views/emp-back-to-unit/empBackToUnitAdd.html',
                    controller: 'empBackToUnitAddController as vm',
                    title: 'Employee Back to Unit'
                }
            },


            //-------------Employee Rejoin-------
            {
                state: 'employee-rejoins',
                config: {
                    url: '/employee-rejoins?ps&pn&q',
                    templateUrl: 'app/views/employee-rejoin/empRejoins.html',
                    controller: 'empRejoinsController as vm',
                    title: 'Employee Rejoin'
                }
            },
            {
                state: 'employee-rejoin-create',
                config: {
                    url: '/employee-rejoin-create',
                    templateUrl: 'app/views/employee-rejoin/empRejoinAdd.html',
                    controller: 'empRejoinAddController as vm',
                    title: 'Employee Rejoin'
                }
            },
            {
                state: "employee-rejoin-modify",
                config: {
                    url: "employee-rejoin-modify/:id",
                    templateUrl: 'app/views/employee-rejoin/empRejoinAdd.html',
                    controller: 'empRejoinAddController as vm',
                    title: 'Employee Rejoin'
                }
            },
            //-------------Exam Subjects-------
            {
                state: 'exam-subjects',
                config: {
                    url: '/exam-subjects?ps&pn&q',
                    templateUrl: 'app/views/exam-subject/examSubjects.html',
                    controller: 'examSubjectsController as vm',
                    title: 'Exam Subject'
                }
            },
            {
                state: 'exam-subject-create',
                config: {
                    url: '/exam-subject-create',
                    templateUrl: 'app/views/exam-subject/examSubjectAdd.html',
                    controller: 'examSubjectAddController as vm',
                    title: 'Exam Subject'
                }
            },
            {
                state: "exam-subject-modify",
                config: {
                    url: 'exam-subject-modify/:id',
                    templateUrl: 'app/views/exam-subject/examSubjectAdd.html',
                    controller: 'examSubjectAddController as vm',
                    title: 'Exam Subject'
                }
            },
            //-------------Result-------
            {
                state: 'results',
                config: {
                    url: '/results?ps&pn&q',
                    templateUrl: 'app/views/result/results.html',
                    controller: 'resultsController as vm',
                    title: 'Result'
                }
            },
            {
                state: 'result-create',
                config: {
                    url: '/result-create',
                    templateUrl: 'app/views/result/resultAdd.html',
                    controller: 'resultAddController as vm',
                    title: 'Result'
                }
            },
            {
                state: "result-modify",
                config: {
                    url: 'result-modify/:id',
                    templateUrl: 'app/views/result/resultAdd.html',
                    controller: 'resultAddController as vm',
                    title: 'Result'
                }
            },

            //-------------Result Type-------
            {
                state: 'result-types',
                config: {
                    url: '/result-types?ps&pn&q',
                    templateUrl: 'app/views/result-type/resultTypes.html',
                    controller: 'resultTypesController as vm',
                    title: 'Result Type'
                }
            },
            {
                state: 'result-type-create',
                config: {
                    url: '/result-type-create',
                    templateUrl: 'app/views/result-type/resultTypeAdd.html',
                    controller: 'resultTypeAddController as vm',
                    title: 'Result Type'
                }
            },
            {
                state: "result-type-modify",
                config: {
                    url: 'result-modify/:id',
                    templateUrl: 'app/views/result-type/resultTypeAdd.html',
                    controller: 'resultTypeAddController as vm',
                    title: 'Result Type'
                }
            },


            //-------------Course-------
            {
                state: 'courses',
                config: {
                    url: '/courses?ps&pn&q',
                    templateUrl: 'app/views/course/courses.html',
                    controller: 'coursesController as vm',
                    title: 'Course'
                }
            },
            {
                state: 'course-create',
                config: {
                    url: '/course-create',
                    templateUrl: 'app/views/course/courseAdd.html',
                    controller: 'courseAddController as vm',
                    title: 'Course'
                }
            },
            {
                state: "course-modify",
                config: {
                    url: 'course-modify/:id',
                    templateUrl: 'app/views/course/courseAdd.html',
                    controller: 'courseAddController as vm',
                    title: 'Course'
                }
            },

            //-------------Training Institute-------
            {
                state: 'training-institutes',
                config: {
                    url: '/training-institutes?ps&pn&q',
                    templateUrl: 'app/views/training-institute/trainingInstitutes.html',
                    controller: 'trainingInstitutesController as vm',
                    title: 'Training Institute'
                }
            },
            {
                state: 'training-institute-create',
                config: {
                    url: '/training-institute-create',
                    templateUrl: 'app/views/training-institute/trainingInstituteAdd.html',
                    controller: 'trainingInstituteAddController as vm',
                    title: 'Training Institute'
                }
            },
            {
                state: "training-institute-modify",
                config: {
                    url: 'training-institute-modify/:id',
                    templateUrl: 'app/views/training-institute/trainingInstituteAdd.html',
                    controller: 'trainingInstituteAddController as vm',
                    title: 'Training Institute'
                }
            },

            //-------------Training Plan-------
            {
                state: 'training-plans',
                config: {
                    url: '/training-plans?ps&pn&q',
                    templateUrl: 'app/views/training-plan/trainingPlans.html',
                    controller: 'trainingPlansController as vm',
                    title: 'Training Plan'
                }
            },
            {
                state: 'training-plan-create',
                config: {
                    url: '/training-plan-create',
                    templateUrl: 'app/views/training-plan/trainingPlanAdd.html',
                    controller: 'trainingPlanAddController as vm',
                    title: 'Training Plan'
                }
            },
            {
                state: "training-plan-modify",
                config: {
                    url: 'training-plan-modify/:id',
                    templateUrl: 'app/views/training-plan/trainingPlanAdd.html',
                    controller: 'trainingPlanAddController as vm',
                    title: 'Training Plan'
                }
            },


            //-------------Training Result-------
            {
                state: 'training-results',
                config: {
                    url: '/training-results?ps&pn&q',
                    templateUrl: 'app/views/training-result/trainingResults.html',
                    controller: 'trainingResultsController as vm',
                    title: 'Training Result'
                }
            },
            {
                state: 'training-result-create',
                config: {
                    url: '/training-result-create',
                    templateUrl: 'app/views/training-result/trainingResultAdd.html',
                    controller: 'trainingResultAddController as vm',
                    title: 'Training Result'
                }
            },
            {
                state: "training-result-modify",
                config: {
                    url: 'training-result-modify/:id',
                    templateUrl: 'app/views/training-result/trainingResultAdd.html',
                    controller: 'trainingResultAddController as vm',
                    title: 'Training Result'
                }
            },

            {
                state: "training-result-upload",
                config: {
                    url: 'training-result-upload/:id',
                    templateUrl: 'app/views/training-result/trainingResultUpload.html',
                    controller: 'trainingResultUploadController as vm',
                    title: 'Training Result'
                }
            },

            //------------- Extracurricular Type-------
            {
                state: 'extracurricular-types',
                config: {
                    url: '/extracurricular-types?ps&pn&q',
                    templateUrl: 'app/views/extracurricular-type/extracurricularTypes.html',
                    controller: 'extracurricularTypesController as vm',
                    title: 'Extracurricular Type'
                }
            },
            {
                state: 'extracurricular-type-create',
                config: {
                    url: '/extracurricular-type-create',
                    templateUrl: 'app/views/extracurricular-type/extracurricularTypeAdd.html',
                    controller: 'extracurricularTypeAddController as vm',
                    title: 'Extracurricular Type'
                }
            },
            {
                state: "extracurricular-type-modify",
                config: {
                    url: "/extracurricular-type-modify/:id",
                    templateUrl: 'app/views/extracurricular-type/extracurricularTypeAdd.html',
                    controller: 'extracurricularTypeAddController as vm',
                    title: 'Extracurricular Type'
                }
            },
            //------------- Religion Cast------
            {
                state: 'religion-casts',
                config: {
                    url: '/religion-casts?ps&pn&q',
                    templateUrl: 'app/views/religion-cast/religionCasts.html',
                    controller: 'religionCastsController as vm',
                    title: 'Religion Cast'
                }
            },
            {
                state: 'religion-cast-create',
                config: {
                    url: '/religion-cast-create',
                    templateUrl: 'app/views/religion-cast/religionCastAdd.html',
                    controller: 'religionCastAddController as vm',
                    title: 'Religion Cast'
                }
            },
            {
                state: "religion-cast-modify",
                config: {
                    url: "/religion-cast-modify/:id",
                    templateUrl: 'app/views/religion-cast/religionCastAdd.html',
                    controller: 'religionCastAddController as vm',
                    title: 'Religion Cast'
                }
            },

            //-------------Occupation-------
            {
                state: 'occupations',
                config: {
                    url: '/occupations?ps&pn&q',
                    templateUrl: 'app/views/occupation/occupations.html',
                    controller: 'occupationsController as vm',
                    title: 'Occupation'
                }
            },
            {
                state: 'occupation-create',
                config: {
                    url: '/occupation-create',
                    templateUrl: 'app/views/occupation/occupationAdd.html',
                    controller: 'occupationAddController as vm',
                    title: 'Occupation'
                }
            },
            {
                state: "occupation-modify",
                config: {
                    url: 'occupation-modify/:id',
                    templateUrl: 'app/views/occupation/occupationAdd.html',
                    controller: 'occupationAddController as vm',
                    title: 'Occupation'
                }
            },

            //-------------Pre Commission Rank-------
            {
                state: 'pre-commission-ranks',
                config: {
                    url: '/pre-commission-ranks?ps&pn&q',
                    templateUrl: 'app/views/pre-commission-rank/preCommissionRanks.html',
                    controller: 'preCommissionRanksController as vm',
                    title: 'Pre Commission Rank'
                }
            },
            {
                state: 'pre-commission-rank-create',
                config: {
                    url: '/pre-commission-rank-create',
                    templateUrl: 'app/views/pre-commission-rank/preCommissionRankAdd.html',
                    controller: 'preCommissionRankAddController as vm',
                    title: 'Pre Commission Rank'
                }
            },
            {
                state: "pre-commission-rank-modify",
                config: {
                    url: 'pre-commission-rank-modify/:id',
                    templateUrl: 'app/views/pre-commission-rank/preCommissionRankAdd.html',
                    controller: 'preCommissionRankAddController as vm',
                    title: 'Pre Commission Rank'
                }
            },

            //-------------Relation-------
            {
                state: 'relations',
                config: {
                    url: '/relations?ps&pn&q',
                    templateUrl: 'app/views/relation/relations.html',
                    controller: 'relationsController as vm',
                    title: 'Relation'
                }
            },
            {
                state: 'relation-create',
                config: {
                    url: '/relation-create',
                    templateUrl: 'app/views/relation/relationAdd.html',
                    controller: 'relationAddController as vm',
                    title: 'Relation'
                }
            },
            {
                state: "relation-modify",
                config: {
                    url: 'relation-modify/:id',
                    templateUrl: 'app/views/relation/relationAdd.html',
                    controller: 'relationAddController as vm',
                    title: 'Relation'
                }
            },

            //-------------Heir Type-------
            {
                state: 'heir-types',
                config: {
                    url: '/heir-types?ps&pn&q',
                    templateUrl: 'app/views/heir-type/heirTypes.html',
                    controller: 'heirTypesController as vm',
                    title: 'Heir Type'
                }
            },
            {
                state: 'heir-type-create',
                config: {
                    url: '/heir-type-create',
                    templateUrl: 'app/views/heir-type/heirTypeAdd.html',
                    controller: 'heirTypeAddController as vm',
                    title: 'Heir Type'
                }
            },
            {
                state: "heir-type-modify",
                config: {
                    url: 'heir-type-modify/:id',
                    templateUrl: 'app/views/heir-type/heirTypeAdd.html',
                    controller: 'heirTypeAddController as vm',
                    title: 'Heir Type'
                }
            },
            //-------------Appointment Nature-------
            {
                state: 'appointmentNatures',
                config: {
                    url: '/appointmentNatures?ps&pn&q',
                    templateUrl: 'app/views/appointment-Nature/appointmentNatures.html',
                    controller: 'appointmentNaturesController as vm',
                    title: 'AppointmentNature'
                }
            },
            {
                state: 'appointmentNature-create',
                config: {
                    url: '/appointmentNature-create',
                    templateUrl: 'app/views/appointment-Nature/appointmentNatureAdd.html',
                    controller: 'appointmentNatureAddController as vm',
                    title: 'AppointmentNature'
                }
            },
            {
                state: "appointmentNature-modify",
                config: {
                    url: 'appointmentNature-modify/:id',
                    templateUrl: 'app/views/appointment-Nature/appointmentNatureAdd.html',
                    controller: 'appointmentNatureAddController as vm',
                    title: 'AppointmentNature'
                }
            },
            //-------------Appointment Category-------
            {
                state: 'appointmentCategories',
                config: {
                    url: '/appointmentCategories?ps&pn&q',
                    templateUrl: 'app/views/appointment-Category/appointmentCategories.html',
                    controller: 'appointmentCategoriesController as vm',
                    title: 'AppointmentCategory'
                }
            },
            {
                state: 'appointmentCategory-create',
                config: {
                    url: '/appointmentCategory-create',
                    templateUrl: 'app/views/appointment-Category/appointmentCategoriesAdd.html',
                    controller: 'appointmentCategoriesAddController as vm',
                    title: 'AppointmentCategory'
                }
            },
            {
                state: "appointmentCategory-modify",
                config: {
                    url: 'appointmentCategory-modify/:id',
                    templateUrl: 'app/views/appointment-Category/appointmentCategoriesAdd.html',
                    controller: 'appointmentCategoriesAddController as vm',
                    title: 'AppointmentCategory'
                }
            },
            //------------- Pattern -------
            {
                state: 'patterns',
                config: {
                    url: '/patterns?ps&pn&q',
                    templateUrl: 'app/views/pattern/patterns.html',
                    controller: 'patternsController as vm',
                    title: 'Pattern'
                }
            },
            {
                state: 'pattern-create',
                config: {
                    url: '/pattern-create',
                    templateUrl: 'app/views/pattern/patternAdd.html',
                    controller: 'patternAddController as vm',
                    title: 'Pattern'
                }
            },
            {
                state: "pattern-modify",
                config: {
                    url: 'pattern-modify/:id',
                    templateUrl: 'app/views/pattern/patternAdd.html',
                    controller: 'patternAddController as vm',
                    title: 'Pattern'
                }
            },

            //------------- Zone -------
            {
                state: 'zones',
                config: {
                    url: '/zones?ps&pn&q',
                    templateUrl: 'app/views/zone/zones.html',
                    controller: 'zonesController as vm',
                    title: 'Zone'
                }
            },
            {
                state: 'zone-create',
                config: {
                    url: '/zone-create',
                    templateUrl: 'app/views/zone/zoneAdd.html',
                    controller: 'zoneAddController as vm',
                    title: 'Zone'
                }
            },
            {
                state: "zone-modify",
                config: {
                    url: 'zone-modify/:id',
                    templateUrl: 'app/views/zone/zoneAdd.html',
                    controller: 'zoneAddController as vm',
                    title: 'Zone'
                }
            },

            //------------- Ship Category -------
            {
                state: 'ship-categories',
                config: {
                    url: '/ship-categories?ps&pn&q',
                    templateUrl: 'app/views/ship-category/shipCategories.html',
                    controller: 'shipCategoriesController as vm',
                    title: 'Ship Category'
                }
            },
            {
                state: 'ship-category-create',
                config: {
                    url: '/ship-category-create',
                    templateUrl: 'app/views/ship-category/shipCategoryAdd.html',
                    controller: 'shipCategoryAddController as vm',
                    title: 'Ship Category'
                }
            },
            {
                state: "ship-category-modify",
                config: {
                    url: 'ship-category-modify/:id',
                    templateUrl: 'app/views/ship-category/shipCategoryAdd.html',
                    controller: 'shipCategoryAddController as vm',
                    title: 'Ship Category'
                }
            },

            //-------------Promotion Board-------
            {
                state: 'promotion-boards',
                config: {
                    url: '/promotion-boards/:type?ps&pn&q',
                    templateUrl: 'app/views/promotion-board/promotionBoards.html',
                    controller: 'promotionBoardsController as vm',
                    title: 'Promotion Board'
                }
            },
            {
                state: 'promotion-board-create',
                config: {
                    url: '/promotion-board-create/:type',
                    templateUrl: 'app/views/promotion-board/promotionBoardAdd.html',
                    controller: 'promotionBoardAddController as vm',
                    title: 'Promotion Board'
                }
            },
            {
                state: "promotion-board-modify",
                config: {
                    url: '/promotion-board-modify/:type/:id',
                    templateUrl: 'app/views/promotion-board/promotionBoardAdd.html',
                    controller: 'promotionBoardAddController as vm',
                    title: 'Promotion Board'
                }
            },
            //-------------Promotion Nomination-------
            {
                state: 'promotion-nominations',
                config: {
                    url: '/promotion-nominations/:type/:title?promotionBoardId&ps&pn&q',
                    templateUrl: 'app/views/promotion-nomination/promotionNominations.html',
                    controller: 'promotionNominationsController as vm',
                    title: 'Promotion Nomination'
                }
            },
            {
                state: 'promotion-nomination-create',
                config: {
                    url: '/promotion-nomination-create/:type/:title?promotionBoardId',
                    templateUrl: 'app/views/promotion-nomination/promotionNominationAdd.html',
                    controller: 'promotionNominationAddController as vm',
                    title: 'Promotion Nomination'
                }
            },
            {
                state: "promotion-nomination-modify",
                config: {
                    url: '/promotion-nomination-modify/:type/:title/:promotionBoardId/:promotionNominationId',
                    templateUrl: 'app/views/promotion-nomination/promotionNominationAdd.html',
                    controller: 'promotionNominationAddController as vm',
                    title: 'Promotion Nomination'
                }
            },
            //-------------Board Members-------
            {
                state: 'board-members',
                config: {
                    url: '/board-members/:type/:title?promotionBoardId&ps&pn&q',
                    templateUrl: 'app/views/board-member/boardMembers.html',
                    controller: 'boardMembersController as vm',
                    title: 'Board Member'
                }
            },
            {
                state: 'board-member-create',
                config: {
                    url: '/board-member-create/:type/:title?promotionBoardId',
                    templateUrl: 'app/views/board-member/boardMemberAdd.html',
                    controller: 'boardMemberAddController as vm',
                    title: 'Board Member'
                }
            },
            {
                state: "board-member-modify",
                config: {
                    url: '/board-member-modify/:type/:title/:promotionBoardId/:boardMemberId',
                    templateUrl: 'app/views/board-member/boardMemberAdd.html',
                    controller: 'boardMemberAddController as vm',
                    title: 'Board Member'
                }
            },

            //-------------Promotion Execution-------
            {
                state: 'promotion-executions',
                config: {
                    url: '/promotion-executions/:type?ps&pn&q',
                    templateUrl: 'app/views/promotion-execution/promotionExecutions.html',
                    controller: 'promotionExecutionsController as vm',
                    title: 'Promotion Execution'
                }
            },
            {
                state: 'promotion-executed-list',
                config: {
                    url: '/promotion-executed-list/:type?promotionBoardId',
                    templateUrl: 'app/views/promotion-execution/promotionExecutedList.html',
                    controller: 'promotionExecutedListController as vm',
                    title: 'Promotion Execution'
                }
            },
            {
                state: "promotion-execution-list-modify",
                config: {
                    url: '/promotion-execution-list-modify/:type/:promotionBoardId',
                    templateUrl: 'app/views/promotion-execution/promotionExecutedListAdd.html',
                    controller: 'promotionExecutedListController as vm',
                    title: 'Promotion Execution'
                }
            },

            //-------------Promotion Execution Without Board-------
            {
                state: 'promotion-execution-without-boards',
                config: {
                    url: '/promotion-execution-without-boards?ps&pn&q',
                    templateUrl: 'app/views/promotion-execution-without-board/promotionExecutionWithoutBoards.html',
                    controller: 'promotionExecutionWithoutBoardsController as vm',
                    title: 'Promotion Execution Without Board'
                }
            },
            {
                state: 'promotion-execution-without-board-create',
                config: {
                    url: '/promotion-execution-without-board-create',
                    templateUrl: 'app/views/promotion-execution-without-board/promotionExecutionWithoutBoardAdd.html',
                    controller: 'promotionExecutionWithoutBoardAddController as vm',
                    title: 'Promotion Execution Without Board'
                }
            },
            {
                state: "promotion-execution-without-board-modify",
                config: {
                    url: '/promotion-execution-without-board-modify/:promotionNominationId',
                    templateUrl: 'app/views/promotion-execution-without-board/promotionExecutionWithoutBoardAdd.html',
                    controller: 'promotionExecutionWithoutBoardAddController as vm',
                    title: 'Promotion Execution Without Board'
                }
            },

           

            //-------------Office Structure-------
            {
                state: 'office-structures',
                config: {
                    url: '/office-structures',
                    templateUrl: 'app/views/office-structure/officeStructures.html',
                    controller: 'officeStructuresController as vm',
                    title: 'Office Structure'
                }
            },


            //-------------Office Appointment Structure-------
            {
                state: 'office-appointment-structures',
                config: {
                    url: '/office-appointment-structures/:officeId',
                    templateUrl: 'app/views/office-appointment-structure/officeAppointmentStructures.html',
                    controller: 'officeAppointmentStructuresController as vm',
                    title: 'Office Appointment Structure'
                }
            },


            //-------------Office Tab-------
            {
                state: 'office-tabs',
                config: {
                    url: '/office-tabs?officeId',
                    templateUrl: 'app/views/officeTab/index.html',
                    controller: 'officeTabController as vm',
                    title: 'Office Details'
                }
            },

            //-------------Office-------
            {
                state: 'office-tabs.office-general',
                config: {
                    url: '/general/:id',
                    templateUrl: 'app/views/office/office.html',
                    controller: 'officeController as vm',
                    title: 'Office'
                }
            },
            //-------------Office Appointment-------
            {
                state: 'office-tabs.office-appointments',
                config: {
                    url: '/office-appointments/:id?ps&pn&q',
                    templateUrl: 'app/views/office-appointment/officeAppointments.html',
                    controller: 'officeAppointmentsController as vm',
                    title: 'Office Appointment'
                }
            },
            {
                state: 'office-tabs.office-appointment-create',
                config: {
                    url: '/office-appointment-create/:id/:appointmentId',
                    templateUrl: 'app/views/office-appointment/officeAppointmentAdd.html',
                    controller: 'officeAppointmentAddController as vm',
                    title: 'Office Appointment'
                }
            },
            {
                state: "office-tabs.office-appointment-modify",
                config: {
                    url: '/office-appointment-modify/:id/:appointmentId',
                    templateUrl: 'app/views/office-appointment/officeAppointmentAdd.html',
                    controller: 'officeAppointmentAddController as vm',
                    title: 'Office Appointment'
                }
            },



            //-------------Office Additional Appointment-------
            {
                state: 'office-tabs.office-additional-appointments',
                config: {
                    url: '/office-additional-appointments/:id?ps&pn&q',
                    templateUrl: 'app/views/office-additional-appointment/officeAdditionalAppointments.html',
                    controller: 'officeAdditionalAppointmentsController as vm',
                    title: 'Office Additional Appointment'
                }
            },
            {
                state: 'office-tabs.office-additional-appointment-create',
                config: {
                    url: '/office-additional-appointment-create/:id/:appointmentId',
                    templateUrl: 'app/views/office-additional-appointment/officeAdditionalAppointmentAdd.html',
                    controller: 'officeAdditionalAppointmentAddController as vm',
                    title: 'Office Additional Appointment'
                }
            },
            {
                state: "office-tabs.office-additional-appointment-modify",
                config: {
                    url: '/office-additional-appointment-modify/:id/:appointmentId',
                    templateUrl: 'app/views/office-additional-appointment/officeAdditionalAppointmentAdd.html',
                    controller: 'officeAdditionalAppointmentAddController as vm',
                    title: 'Office Additional Appointment'
                }
            },




            //------------- Lpr Calculate Info -------
            {
                state: 'lprCalculateInfoes',
                config: {
                    url: '/lprCalculateInfoes?ps&pn&q',
                    templateUrl: 'app/views/lpr-calculateInfo/lprCalculateInfoes.html',
                    controller: 'lprCalculateInfoesController as vm',
                    title: 'Lpr Calculate'
                }
            },
            {
                state: 'lprCalculateInfo-create',
                config: {
                    url: '/lprCalculateInfo-create',
                    templateUrl: 'app/views/lpr-calculateInfo/lprCalculateInfoAdd.html',
                    controller: 'lprCalculateInfoAddController as vm',
                    title: 'Lpr Calculate'
                }
            },
            {
                state: "lprCalculateInfo-modify",
                config: {
                    url: 'lprCalculateInfo-modify/:id',
                    templateUrl: 'app/views/lpr-calculateInfo/lprCalculateInfoAdd.html',
                    controller: 'lprCalculateInfoAddController as vm',
                    title: 'Lpr Calculate'
                }
            },
            //------------- Leave Break Down -------
            {
                state: 'leavebreakdowns',
                config: {
                    url: '/leavebreakdowns',
                    templateUrl: 'app/views/leave-break-down/leaveBreakDowns.html',
                    controller: 'leaveBreakDownsController as vm',
                    title: 'Leave Break Down'
                }
            },

            //-------------Medal-------
            {
                state: 'medals',
                config: {
                    url: '/medals?ps&pn&q',
                    templateUrl: 'app/views/medal/medals.html',
                    controller: 'medalsController as vm',
                    title: 'Medal'
                }
            },
            {
                state: 'medal-create',
                config: {
                    url: '/medal-create',
                    templateUrl: 'app/views/medal/medalAdd.html',
                    controller: 'medalAddController as vm',
                    title: 'Medal'
                }
            },
            {
                state: "medal-modify",
                config: {
                    url: "medal-modify/:id",
                    templateUrl: 'app/views/medal/medalAdd.html',
                    controller: 'medalAddController as vm',
                    title: 'Medal'
                }
            },

            //-------------Award-------
            {
                state: 'awards',
                config: {
                    url: '/awards?ps&pn&q',
                    templateUrl: 'app/views/award/awards.html',
                    controller: 'awardsController as vm',
                    title: 'Award'
                }
            },
            {
                state: 'award-create',
                config: {
                    url: '/award-create',
                    templateUrl: 'app/views/award/awardAdd.html',
                    controller: 'awardAddController as vm',
                    title: 'Award'
                }
            },
            {
                state: "award-modify",
                config: {
                    url: "award-modify/:id",
                    templateUrl: 'app/views/award/awardAdd.html',
                    controller: 'awardAddController as vm',
                    title: 'Award'
                }
            },

            //-------------Publication Category-------
            {
                state: 'publication-categories',
                config: {
                    url: '/publication-categories?ps&pn&q',
                    templateUrl: 'app/views/publication-category/publicationCategories.html',
                    controller: 'publicationCategoriesController as vm',
                    title: 'Publication Category'
                }
            },
            {
                state: 'publication-category-create',
                config: {
                    url: '/publication-category-create',
                    templateUrl: 'app/views/publication-category/publicationCategoryAdd.html',
                    controller: 'publicationCategoryAddController as vm',
                    title: 'Publication Category'
                }
            },
            {
                state: "publication-category-modify",
                config: {
                    url: "publication-category-modify/:id",
                    templateUrl: 'app/views/publication-category/publicationCategoryAdd.html',
                    controller: 'publicationCategoryAddController as vm',
                    title: 'Publication Category'
                }
            },


            //-------------Publication-------
            {
                state: 'publications',
                config: {
                    url: '/publications?ps&pn&q',
                    templateUrl: 'app/views/publication/publications.html',
                    controller: 'publicationsController as vm',
                    title: 'Publication'
                }
            },
            {
                state: 'publication-create',
                config: {
                    url: '/publication-create',
                    templateUrl: 'app/views/publication/publicationAdd.html',
                    controller: 'publicationAddController as vm',
                    title: 'Publication'
                }
            },
            {
                state: "publication-modify",
                config: {
                    url: "publication-modify/:id",
                    templateUrl: 'app/views/publication/publicationAdd.html',
                    controller: 'publicationAddController as vm',
                    title: 'Publication'
                }
            },

            //-------------Punishment Category-------
            {
                state: 'punishment-categories',
                config: {
                    url: '/punishment-categories?ps&pn&q',
                    templateUrl: 'app/views/punishment-category/punishmentCategories.html',
                    controller: 'punishmentCategoriesController as vm',
                    title: 'Punishment Category'
                }
            },
            {
                state: 'punishment-category-create',
                config: {
                    url: '/punishment-category-create',
                    templateUrl: 'app/views/punishment-category/punishmentCategoryAdd.html',
                    controller: 'punishmentCategoryAddController as vm',
                    title: 'Punishment Category'
                }
            },
            {
                state: "punishment-category-modify",
                config: {
                    url: "punishment-category-modify/:id",
                    templateUrl: 'app/views/punishment-category/punishmentCategoryAdd.html',
                    controller: 'punishmentCategoryAddController as vm',
                    title: 'Punishment Category'
                }
            },

            //-------------Punishment Sub Category-------
            {
                state: 'punishment-sub-categories',
                config: {
                    url: '/punishment-sub-categories?ps&pn&q',
                    templateUrl: 'app/views/punishment-sub-category/punishmentSubCategories.html',
                    controller: 'punishmentSubCategoriesController as vm',
                    title: 'Punishment Sub Category'
                }
            },
            {
                state: 'punishment-sub-category-create',
                config: {
                    url: '/punishment-sub-category-create',
                    templateUrl: 'app/views/punishment-sub-category/punishmentSubCategoryAdd.html',
                    controller: 'punishmentSubCategoryAddController as vm',
                    title: 'Punishment Sub Category'
                }
            },
            {
                state: "punishment-sub-category-modify",
                config: {
                    url: "punishment-sub-category-modify/:id",
                    templateUrl: 'app/views/punishment-sub-category/punishmentSubCategoryAdd.html',
                    controller: 'punishmentSubCategoryAddController as vm',
                    title: 'Punishment Sub Category'
                }
            },

            //-------------Commendation-------
            {
                state: 'commendations',
                config: {
                    url: '/commendations?ps&pn&q',
                    templateUrl: 'app/views/commendation/commendations.html',
                    controller: 'commendationsController as vm',
                    title: 'Commendation'
                }
            },
            {
                state: 'commendation-create',
                config: {
                    url: '/commendation-create',
                    templateUrl: 'app/views/commendation/commendationAdd.html',
                    controller: 'commendationAddController as vm',
                    title: 'Commendation'
                }
            },
            {
                state: "commendation-modify",
                config: {
                    url: "commendation-modify/:id",
                    templateUrl: 'app/views/commendation/commendationAdd.html',
                    controller: 'commendationAddController as vm',
                    title: 'Commendation'
                }
            },

            //-------------Punishment Nature-------
            {
                state: 'punishment-natures',
                config: {
                    url: '/punishment-natures?ps&pn&q',
                    templateUrl: 'app/views/punishment-nature/punishmentNatures.html',
                    controller: 'punishmentNaturesController as vm',
                    title: 'Punishment Nature'
                }
            },
            {
                state: 'punishment-nature-create',
                config: {
                    url: '/punishment-nature-create',
                    templateUrl: 'app/views/punishment-nature/punishmentNatureAdd.html',
                    controller: 'punishmentNatureAddController as vm',
                    title: 'Punishment Nature'
                }
            },
            {
                state: "punishment-nature-modify",
                config: {
                    url: "punishment-nature-modify/:id",
                    templateUrl: 'app/views/punishment-nature/punishmentNatureAdd.html',
                    controller: 'punishmentNatureAddController as vm',
                    title: 'Punishment Nature'
                }
            },

            //-------------Achievement-------
            {
                state: 'achievements',
                config: {
                    url: '/achievements?ps&pn&q',
                    templateUrl: 'app/views/achievement/achievements.html',
                    controller: 'achievementsController as vm',
                    title: 'Achievement'
                }
            },
            {
                state: 'achievement-create',
                config: {
                    url: '/achievement-create',
                    templateUrl: 'app/views/achievement/achievementAdd.html',
                    controller: 'achievementAddController as vm',
                    title: 'Achievement'
                }
            },
            {
                state: "achievement-modify",
                config: {
                    url: "achievement-modify/:id",
                    templateUrl: 'app/views/achievement/achievementAdd.html',
                    controller: 'achievementAddController as vm',
                    title: 'Achievement'
                }
            },
            //------------- Employee LPR -------
            {
                state: 'employeelpr',
                config: {
                    url: '/employee-lpr?ps&pn&q',
                    templateUrl: 'app/views/employee-lpr/employeeLprs.html',
                    controller: 'employeeLprController as vm',
                    title: 'Employee LPR'
                }
            },
            {
                state: 'employee-lpr-create',
                config: {
                    url: '/employee-lpr-create',
                    templateUrl: 'app/views/employee-lpr/employeeLprAdd.html',
                    controller: 'employeeLprAddController as vm',
                    title: 'Employee LPR Create'
                }
            },
            {
                state: "employee-lpr-modify",
                config: {
                    url: "employee-lpr-modify-modify/:id",
                    templateUrl: 'app/views/employee-lpr/employeeLprAdd.html',
                    controller: 'employeeLprAddController as vm',
                    title: 'Employee LPR Create'
                }
            },


            //-------------Medal Award-------
            {
                state: 'medal-awards',
                config: {
                    url: '/medal-awards?ps&pn&q',
                    templateUrl: 'app/views/medal-award/medalAwards.html',
                    controller: 'medalAwardsController as vm',
                    title: 'Medal Award'
                }
            },
            {
                state: 'medal-award-create',
                config: {
                    url: '/medal-award-create',
                    templateUrl: 'app/views/medal-award/medalAwardAdd.html',
                    controller: 'medalAwardAddController as vm',
                    title: 'Medal Award'
                }
            },
            {
                state: "medal-award-modify",
                config: {
                    url: "medal-award-modify/:id",
                    templateUrl: 'app/views/medal-award/medalAwardAdd.html',
                    controller: 'medalAwardAddController as vm',
                    title: 'Medal Award'
                }
            },

            //-------------Observation Intelligent Report-------
            {
                state: 'observation-intelligent-reports',
                config: {
                    url: '/observation-intelligent-reports?ps&pn&q',
                    templateUrl: 'app/views/observation-intelligent/observationIntelligents.html',
                    controller: 'observationIntelligentsController as vm',
                    title: 'Observation Intelligent Report'
                }
            },
            {
                state: 'observation-intelligent-report-create',
                config: {
                    url: '/observation-intelligent-report-create',
                    templateUrl: 'app/views/observation-intelligent/observationIntelligentAdd.html',
                    controller: 'observationIntelligentAddController as vm',
                    title: 'Observation Intelligent Report'
                }
            },
            {
                state: "observation-intelligent-report-modify",
                config: {
                    url: "observation-intelligent-report-modify/:id",
                    templateUrl: 'app/views/observation-intelligent/observationIntelligentAdd.html',
                    controller: 'observationIntelligentAddController as vm',
                    title: 'Observation Intelligent Report'
                }
            },


            //-------------Punishment Accident-------
            {
                state: 'punishment-accidents',
                config: {
                    url: '/punishment-accidents?ps&pn&q',
                    templateUrl: 'app/views/punishment-accident/punishmentAccidents.html',
                    controller: 'punishmentAccidentsController as vm',
                    title: 'Punishment Accident'
                }
            },
            {
                state: 'punishment-accident-create',
                config: {
                    url: '/punishment-accident-create',
                    templateUrl: 'app/views/punishment-accident/punishmentAccidentAdd.html',
                    controller: 'punishmentAccidentAddController as vm',
                    title: 'Punishment Accident'
                }
            },
            {
                state: "punishment-accident-modify",
                config: {
                    url: "punishment-accident-modify/:id",
                    templateUrl: 'app/views/punishment-accident/punishmentAccidentAdd.html',
                    controller: 'punishmentAccidentAddController as vm',
                    title: 'Punishment Accident'
                }
            },


            //-------------Nomination-------
            {
                state: 'nominations',
                config: {
                    url: '/nominations/:type?ps&pn&q',
                    templateUrl: 'app/views/nomination/nominations.html',
                    controller: 'nominationsController as vm',
                    title: 'Nomination'
                }
            },
            {
                state: 'nomination-create',
                config: {
                    url: '/nomination-create/:type',
                    templateUrl: 'app/views/nomination/nominationAdd.html',
                    controller: 'nominationAddController as vm',
                    title: 'Nomination'
                }
            },
            {
                state: "nomination-modify",
                config: {
                    url: "nomination-modify/:type/:id",
                    templateUrl: 'app/views/nomination/nominationAdd.html',
                    controller: 'nominationAddController as vm',
                    title: 'Nomination'
                }
            },

            //-------------Nomination Details-------
            {
                state: 'nomination-details',
                config: {
                    url: '/nomination-details/:type/:id/:title',
                    templateUrl: 'app/views/nomination-detail/nominationDetails.html',
                    controller: 'nominationDetailsController as vm',
                    title: 'Nomination Details'
                }
            },

            //-------------Nomination Approval-------
            {
                state: 'nomination-approvals',
                config: {
                    url: '/nomination-approvals',
                    templateUrl: 'app/views/nomination-detail/nominationApprovals.html',
                    controller: 'nominationApprovalsController as vm',
                    title: 'Nomination Details'

                }
            },


            //-------------Office Transfer-------
            {
                state: 'office-transfer',
                config: {
                    url: '/office-transfer',
                    templateUrl: 'app/views/office-transfer/officeTransfers.html',
                    controller: 'officeTransfersController as vm',
                    title: 'Office Transfer'
                }
            },

            //-------------Course Transfer-------
            {
                state: 'course-transfer',
                config: {
                    url: '/course-transfer',
                    templateUrl: 'app/views/course-transfer/courseTransfers.html',
                    controller: 'courseTransfersController as vm',
                    title: 'Course Transfer'
                }
            },

            //-------------Mission Transfer-------
            {
                state: 'mission-transfer',
                config: {
                    url: '/mission-transfer',
                    templateUrl: 'app/views/mission-transfer/missionTransfers.html',
                    controller: 'missionTransfersController as vm',
                    title: 'Mission Transfer'
                }
            },


            //-------------Mission Schedule-------
            {
                state: 'mission-schedules',
                config: {
                    url: '/mission-schedules?ps&pn&q',
                    templateUrl: 'app/views/mission-schedule/missionSchedules.html',
                    controller: 'missionSchedulesController as vm',
                    title: 'Mission Schedule'
                }
            },
            {
                state: 'mission-schedule-create',
                config: {
                    url: '/mission-schedule-create',
                    templateUrl: 'app/views/mission-schedule/missionScheduleAdd.html',
                    controller: 'missionScheduleAddController as vm',
                    title: 'Mission Schedule'
                }
            },
            {
                state: "mission-schedule-modify",
                config: {
                    url: "mission-schedule-modify/:id",
                    templateUrl: 'app/views/mission-schedule/missionScheduleAdd.html',
                    controller: 'missionScheduleAddController as vm',
                    title: 'Mission Schedule'
                }
            },


            //-------------Visit Category-------
            {
                state: 'visit-categories',
                config: {
                    url: '/visit-categories?ps&pn&q',
                    templateUrl: 'app/views/visit-category/visitCategories.html',
                    controller: 'visitCategoriesController as vm',
                    title: 'Visit Category'
                }
            },
            {
                state: 'visit-category-create',
                config: {
                    url: '/visit-category-create',
                    templateUrl: 'app/views/visit-category/visitCategoryAdd.html',
                    controller: 'visitCategoryAddController as vm',
                    title: 'Visit Category'
                }
            },
            {
                state: "visit-category-modify",
                config: {
                    url: "visit-category-modify/:id",
                    templateUrl: 'app/views/visit-category/visitCategoryAdd.html',
                    controller: 'visitCategoryAddController as vm',
                    title: 'Visit Category'
                }
            },

            //-------------Punishment Sub Category-------
            {
                state: 'visit-sub-categories',
                config: {
                    url: '/visit-sub-categories?ps&pn&q',
                    templateUrl: 'app/views/visit-sub-category/visitSubCategories.html',
                    controller: 'visitSubCategoriesController as vm',
                    title: 'Visit Sub Category'
                }
            },
            {
                state: 'visit-sub-category-create',
                config: {
                    url: '/visit-sub-category-create',
                    templateUrl: 'app/views/visit-sub-category/visitSubCategoryAdd.html',
                    controller: 'visitSubCategoryAddController as vm',
                    title: 'Visit Sub Category'
                }
            },
            {
                state: "visit-sub-category-modify",
                config: {
                    url: "visit-sub-category-modify/:id",
                    templateUrl: 'app/views/visit-sub-category/visitSubCategoryAdd.html',
                    controller: 'visitSubCategoryAddController as vm',
                    title: 'Visit Sub Category'
                }
            },


            //-------------Mission Appointment-------
            {
                state: 'mission-appointments',
                config: {
                    url: '/mission-appointments?ps&pn&q',
                    templateUrl: 'app/views/mission-appointment/missionAppointments.html',
                    controller: 'missionAppointmentsController as vm',
                    title: 'Mission Appointment'
                }
            },
            {
                state: 'mission-appointment-create',
                config: {
                    url: '/mission-appointment-create',
                    templateUrl: 'app/views/mission-appointment/missionAppointmentAdd.html',
                    controller: 'missionAppointmentAddController as vm',
                    title: 'Mission Appointment'
                }
            },
            {
                state: "mission-appointment-modify",
                config: {
                    url: "mission-appointment-modify/:id",
                    templateUrl: 'app/views/mission-appointment/missionAppointmentAdd.html',
                    controller: 'missionAppointmentAddController as vm',
                    title: 'Mission Appointment'
                }
            },


            //-------------Foreign Visit Schedule-------
            {
                state: 'foreign-visit-schedules',
                config: {
                    url: '/foreign-visit-schedules?ps&pn&q',
                    templateUrl: 'app/views/foreign-visit-schedule/foreignVisitSchedules.html',
                    controller: 'foreignVisitSchedulesController as vm',
                    title: 'Foreign Visit Schedule'
                }
            },
            {
                state: 'foreign-visit-schedule-create',
                config: {
                    url: '/foreign-visit-schedule-create',
                    templateUrl: 'app/views/foreign-visit-schedule/foreignVisitScheduleAdd.html',
                    controller: 'foreignVisitScheduleAddController as vm',
                    title: 'Foreign Visit Schedule'
                }
            },
            {
                state: "foreign-visit-schedule-modify",
                config: {
                    url: "foreign-visit-schedule-modify/:id",
                    templateUrl: 'app/views/foreign-visit-schedule/foreignVisitScheduleAdd.html',
                    controller: 'foreignVisitScheduleAddController as vm',
                    title: 'Foreign Visit Schedule'
                }
            },


            //-------------Other Schedule-------
            {
                state: 'other-schedules',
                config: {
                    url: '/other-schedules?ps&pn&q',
                    templateUrl: 'app/views/other-schedule/otherSchedules.html',
                    controller: 'otherSchedulesController as vm',
                    title: 'Other Schedule'
                }
            },
            {
                state: 'other-schedule-create',
                config: {
                    url: '/other-schedule-create',
                    templateUrl: 'app/views/other-schedule/otherScheduleAdd.html',
                    controller: 'otherScheduleAddController as vm',
                    title: 'Other Schedule'
                }
            },
            {
                state: "other-schedule-modify",
                config: {
                    url: "other-schedule-modify/:id",
                    templateUrl: 'app/views/other-schedule/otherScheduleAdd.html',
                    controller: 'otherScheduleAddController as vm',
                    title: 'Other Schedule'
                }
            },


            //-------------Officer Naming Convention-------
            {
                state: 'officer-naming-conventions',
                config: {
                    url: '/officer-naming-conventions?ps&pn&q',
                    templateUrl: 'app/views/officer-naming-convention/officerNamingConventions.html',
                    controller: 'officerNamingConventionsController as vm',
                    title: 'Officer Naming Convention'
                }
            },

            {
                state: "officer-naming-convention-modify",
                config: {
                    url: "officer-naming-convention-modify/:id",
                    templateUrl: 'app/views/officer-naming-convention/officerNamingConventionAdd.html',
                    controller: 'officerNamingConventionAddController as vm',
                    title: 'Officer Naming Convention'
                }
            },



            //-------------Employee Security Clearance-------
            {
                state: 'employee-security-clearances',
                config: {
                    url: '/employee-security-clearances?ps&pn&q',
                    templateUrl: 'app/views/employee-security-clearance/employeeSecurityClearances.html',
                    controller: 'employeeSecurityClearancesController as vm',
                    title: 'Employee Security Clearance'
                }
            },
            {
                state: 'employee-security-clearance-create',
                config: {
                    url: '/employee-security-clearance-create',
                    templateUrl: 'app/views/employee-security-clearance/employeeSecurityClearanceAdd.html',
                    controller: 'employeeSecurityClearanceAddController as vm',
                    title: 'Employee Security Clearance'
                }
            },
            {
                state: "employee-security-clearance-modify",
                config: {
                    url: "employee-security-clearance-modify/:id",
                    templateUrl: 'app/views/employee-security-clearance/employeeSecurityClearanceAdd.html',
                    controller: 'employeeSecurityClearanceAddController as vm',
                    title: 'Employee Security Clearance'
                }
            },

            //------------- Security Clearance Reason-------
            {
                state: 'security-clearance-reasons',
                config: {
                    url: '/security-clearance-reasons?ps&pn&q',
                    templateUrl: 'app/views/security-clearance-reason/securityClearanceReasons.html',
                    controller: 'securityClearanceReasonsController as vm',
                    title: 'Security Clearance Reason'
                }
            },
            {
                state: 'security-clearance-reason-create',
                config: {
                    url: '/security-clearance-reason-create',
                    templateUrl: 'app/views/security-clearance-reason/securityClearanceReasonAdd.html',
                    controller: 'securityClearanceReasonAddController as vm',
                    title: 'Security Clearance Reason'
                }
            },
            {
                state: "security-clearance-reason-modify",
                config: {
                    url: "security-clearance-reason-modify/:id",
                    templateUrl: 'app/views/security-clearance-reason/securityClearanceReasonAdd.html',
                    controller: 'securityClearanceReasonAddController as vm',
                    title: 'Security Clearance Reason'
                }
            },


            //-------------Service Exam Category-------
            {
                state: 'service-exam-categories',
                config: {
                    url: '/service-exam-categories?ps&pn&q',
                    templateUrl: 'app/views/service-exam-category/serviceExamCategories.html',
                    controller: 'serviceExamCategoriesController as vm',
                    title: 'Service Exam Category'
                }
            },
            {
                state: 'service-exam-category-create',
                config: {
                    url: '/service-exam-category-create',
                    templateUrl: 'app/views/service-exam-category/serviceExamCategoryAdd.html',
                    controller: 'serviceExamCategoryAddController as vm',
                    title: 'Service Exam Category'
                }
            },
            {
                state: "service-exam-category-modify",
                config: {
                    url: "service-exam-category-modify/:id",
                    templateUrl: 'app/views/service-exam-category/serviceExamCategoryAdd.html',
                    controller: 'serviceExamCategoryAddController as vm',
                    title: 'Service Exam Category'
                }
            },

            //-------------Service Exam-------
            {
                state: 'service-exams',
                config: {
                    url: '/service-exams?ps&pn&q',
                    templateUrl: 'app/views/service-exam/serviceExams.html',
                    controller: 'serviceExamsController as vm',
                    title: 'Service Exam'
                }
            },
            {
                state: 'service-exam-create',
                config: {
                    url: '/service-exam-create',
                    templateUrl: 'app/views/service-exam/serviceExamAdd.html',
                    controller: 'serviceExamAddController as vm',
                    title: 'Service Exam'
                }
            },
            {
                state: "service-exam-modify",
                config: {
                    url: "service-exam-modify/:id",
                    templateUrl: 'app/views/service-exam/serviceExamAdd.html',
                    controller: 'serviceExamAddController as vm',
                    title: 'Service Exam'
                }
            },




            //-------------Employee Service Exam Result-------
            {
                state: 'employee-service-exam-results',
                config: {
                    url: '/employee-service-exam-results?ps&pn&q',
                    templateUrl: 'app/views/employee-service-exam-result/employeeServiceExamResults.html',
                    controller: 'employeeServiceExamResultsController as vm',
                    title: 'Employee Service Exam Result'
                }
            },
            {
                state: 'employee-service-exam-result-create',
                config: {
                    url: '/employee-service-exam-result-create',
                    templateUrl: 'app/views/employee-service-exam-result/employeeServiceExamResultAdd.html',
                    controller: 'employeeServiceExamResultAddController as vm',
                    title: 'Employee Service Exam Result'
                }
            },
            {
                state: "employee-service-exam-result-modify",
                config: {
                    url: "employee-service-exam-result-modify/:id",
                    templateUrl: 'app/views/employee-service-exam-result/employeeServiceExamResultAdd.html',
                    controller: 'employeeServiceExamResultAddController as vm',
                    title: 'Employee Service Exam Result'
                }
            },

            //-------------Employee PFT-------
            {
                state: 'employee-pfts',
                config: {
                    url: '/employee-pfts?ps&pn&q',
                    templateUrl: 'app/views/employee-pft/employeePfts.html',
                    controller: 'employeePftsController as vm',
                    title: 'Employee PFT'
                }
            },
            {
                state: 'employee-pft-create',
                config: {
                    url: '/employee-pft-create',
                    templateUrl: 'app/views/employee-pft/employeePftAdd.html',
                    controller: 'employeePftAddController as vm',
                    title: 'Employee PFT'
                }
            },
            {
                state: "employee-pft-modify",
                config: {
                    url: "employee-pft-modify/:id",
                    templateUrl: 'app/views/employee-pft/employeePftAdd.html',
                    controller: 'employeePftAddController as vm',
                    title: 'Employee PFT'
                }
            },


            //-------------Current Status Tab-------
            {
                state: 'current-status-tab',
                config: {
                    url: '/current-status-tab',
                    templateUrl: 'app/views/current-status-tab/index.html',
                    controller: 'currentStatusTabController as vm',
                    title: 'Update Officers Detail'
                }
            },




            //-------------Current Status-------
            {
                state: 'current-status-tab.current-status',
                config: {
                    url: '/current-status',
                    templateUrl: 'app/views/current-status/currentStatus.html',
                    controller: 'currentStatusController as vm',
                    title: 'Current Status'
                }
            },

            {
                state: 'employee-current-status',
                config: {
                    url: '/employee-current-status',
                    templateUrl: 'app/views/current-status/currentStatus.html',
                    controller: 'currentStatusController as vm',
                    title: 'Employee Current Status'
                }
            },


            //-------------Pre Commission Test-------
            {
                state: 'current-status-tab.pre-commission-test',
                config: {
                    url: '/pre-commission-test',
                    templateUrl: 'app/views/pre-commission-test/preCommissionTest.html',
                    controller: 'preCommissionTestController as vm',
                    title: 'Pre Commission Test'
                }
            },


            //-------------Mission Foreign Project-------
            {
                state: 'current-status-tab.mission-foreign-projects',
                config: {
                    url: '/mission-foreign-projects',
                    templateUrl: 'app/views/mission-foreign-project/missionForeignProjects.html',
                    controller: 'missionForeignProjectsController as vm',
                    title: 'Mission Foreign Project'
                }
            },




            //-------------Civil Academic Qualification-------
            {
                state: 'current-status-tab.civil-academic-qualification',
                config: {
                    url: '/civil-academic-qualification',
                    templateUrl: 'app/views/civil-academic-qualification/civilAcademicQualification.html',
                    controller: 'civilAcademicQualificationController as vm',
                    title: 'Civil Academic Qualification'
                }
            },
            //-------------Commendation/Appreciation-------
            {
                state: 'current-status-tab.commendation-appreciation',
                config: {
                    url: '/commendation-appreciation',
                    templateUrl: 'app/views/commendation-appreciation/commendationAppreciation.html',
                    controller: 'commendationAppreciationController as vm',
                    title: 'Commendation/Appreciation'
                }
            },
            {
                state: 'current-status-commendation-appreciation',
                config: {
                    url: '/current-status-commendation-appreciation',
                    templateUrl: 'app/views/commendation-appreciation/commendationAppreciation.html',
                    controller: 'commendationAppreciationController as vm',
                    title: 'Commendation/Appreciation'
                }
            },
            //-------------Course Attended-------
            {
                state: 'current-status-tab.course-attended',
                config: {
                    url: '/course-attended',
                    templateUrl: 'app/views/course-attended/courseAttended.html',
                    controller: 'courseAttendedController as vm',
                    title: 'Course Attended'
                }
            },

            {
                state: 'current-status-course-attended',
                config: {
                    url: '/current-status-course-attended',
                    templateUrl: 'app/views/course-attended/courseAttended.html',
                    controller: 'courseAttendedController as vm',
                    title: 'Course Attended'
                }
            },

            //-------------Exam/Test Result-------
            {
                state: 'current-status-tab.exam-test-result',
                config: {
                    url: '/exam-test-result',
                    templateUrl: 'app/views/exam-test-result/examTestResult.html',
                    controller: 'examTestResultController as vm',
                    title: 'Exam/Test Result'
                }
            },

            //-------------Foreign Visit-------
            {
                state: 'current-status-tab.foreign-visit',
                config: {
                    url: '/foreign-visit',
                    templateUrl: 'app/views/foreign-visit/foreignVisit.html',
                    controller: 'foreignVisitController as vm',
                    title: 'Foreign Visit'
                }
            },

            //-------------General Information-------
            {
                state: 'current-status-tab.general-information',
                config: {
                    url: '/general-information',
                    templateUrl: 'app/views/general-information/generalInformation.html',
                    controller: 'generalInformationController as vm',
                    title: 'General Information'
                }
            },

            //-------------Medal,Award & Publication-------
            {
                state: 'current-status-tab.medal-award-publication',
                config: {
                    url: '/medal-award-publication',
                    templateUrl: 'app/views/medal-award-publication/medalAwardPublication.html',
                    controller: 'medalAwardPublicationController as vm',
                    title: 'Medal,Award & Publication'
                }
            },
            {
                state: 'current-status-medal-award-publication',
                config: {
                    url: '/current-status-medal-award-publication',
                    templateUrl: 'app/views/medal-award-publication/medalAwardPublication.html',
                    controller: 'medalAwardPublicationController as vm',
                    title: 'Medal,Award & Publication'
                }
            },

            //-------------OPR Grading-------
            {
                state: 'current-status-tab.opr-grading',
                config: {
                    url: '/opr-grading',
                    templateUrl: 'app/views/opr-grading/oprGrading.html',
                    controller: 'oprGradingController as vm',
                    title: 'OPR Grading'
                }
            },
            {
                state: 'current-status-opr-grading',
                config: {
                    url: '/current-status-opr-grading',
                    templateUrl: 'app/views/opr-grading/oprGrading.html',
                    controller: 'oprGradingController as vm',
                    title: 'OPR Grading'
                }
            },

            //-------------Promotion History-------
            {
                state: 'current-status-tab.promotion-history',
                config: {
                    url: '/promotion-history',
                    templateUrl: 'app/views/promotion-history/promotionHistory.html',
                    controller: 'promotionHistoryController as vm',
                    title: 'Promotion History'
                }
            },

            //-------------Punishment/Discipline-------
            {
                state: 'current-status-tab.punishment-discipline',
                config: {
                    url: '/punishment-discipline',
                    templateUrl: 'app/views/punishment-discipline/punishmentDiscipline.html',
                    controller: 'punishmentDisciplineController as vm',
                    title: 'Punishment/Discipline'
                }
            },

            //-------------Sea Command Services-------
            {
                state: 'current-status-tab.sea-command-services',
                config: {
                    url: '/sea-command-services',
                    templateUrl: 'app/views/sea-command-services/seaCommandServices.html',
                    controller: 'seaCommandServicesController as vm',
                    title: 'Sea Command Services'
                }
            },


            //-------------Zone Services-------
            {
                state: 'current-status-tab.zone-services',
                config: {
                    url: '/zone-services',
                    templateUrl: 'app/views/zone-service/zoneServices.html',
                    controller: 'zoneServicesController as vm',
                    title: 'Zone Services'
                }
            },


            //-------------Intelligence Services-------
            {
                state: 'current-status-tab.intelligence-services',
                config: {
                    url: '/intelligence-services',
                    templateUrl: 'app/views/intelligence-services/intelligenceServices.html',
                    controller: 'intelligenceServicesController as vm',
                    title: 'Intelligence Services'
                }
            },

            //-------------Instructional Services-------
            {
                state: 'current-status-tab.instructional-services',
                config: {
                    url: '/instructional-services',
                    templateUrl: 'app/views/instructional-services/instructionalServices.html',
                    controller: 'instructionalServicesController as vm',
                    title: 'Instructional Services'
                }
            },

            //-------------HOD Services-------
            {
                state: 'current-status-tab.hod-services',
                config: {
                    url: '/hod-services',
                    templateUrl: 'app/views/hod-services/hodServices.html',
                    controller: 'hodServicesController as vm',
                    title: 'HOD Services'
                }
            },

            //-------------Dockyard Services-------
            {
                state: 'current-status-tab.dockyard-services',
                config: {
                    url: '/dockyard-services',
                    templateUrl: 'app/views/dockyard-services/dockyardServices.html',
                    controller: 'dockyardServicesController as vm',
                    title: 'Dockyard Services'
                }
            },


            //-------------Inter Organization Services-------
            {
                state: 'current-status-tab.inter-organization-services',
                config: {
                    url: '/inter-organization-services',
                    templateUrl: 'app/views/inter-organization-services/interOrganizationServices.html',
                    controller: 'interOrganizationServicesController as vm',
                    title: 'Inter Organization Services'
                }
            },

            //-------------Sea Services-------
            {
                state: 'current-status-tab.sea-services',
                config: {
                    url: '/sea-services',
                    templateUrl: 'app/views/sea-services/seaServices.html',
                    controller: 'seaServiceInfoController as vm',
                    title: 'Sea Services'
                }
            },

            //-------------Security Clearance-------
            {
                state: 'current-status-tab.security-clearance',
                config: {
                    url: '/security-clearance',
                    templateUrl: 'app/views/security-clearance/securityClearance.html',
                    controller: 'securityClearanceController as vm',
                    title: 'Security Clearance'
                }
            },

//            //-------------Remark-------
//            {
//                state: 'current-status-tab.remark',
//                config: {
//                    url: '/remark',
//                    templateUrl: 'app/views/remarks/remark.html',
//                    controller: 'remarkController as vm',
//                    title: 'Remark'
//                }
//            },
//            //-------------Persuasion-------
//            {
//                state: 'current-status-tab.persuasion',
//                config: {
//                    url: '/persuasion',
//                    templateUrl: 'app/views/persuasion/persuasion.html',
//                    controller: 'persuasionController as vm',
//                    title: 'Persuasions'
//                }
//            },
//            //-------------Future Plan-------
//            {
//                state: 'current-status-tab.future-plan',
//                config: {
//                    url: '/future-plan',
//                    templateUrl: 'app/views/future-plan/futurePlan.html',
//                    controller: 'futurePlanController as vm',
//                    title: 'Future Plan'
//                }
//            },
       

            //-------------Transfer History-------
            {
                state: 'current-status-tab.transfer-history',
                config: {
                    url: '/transfer-history',
                    templateUrl: 'app/views/transfer-history/transferHistory.html',
                    controller: 'transferHistoryController as vm',
                    title: 'Transfer History'
                }
            },
            {
                state: 'current-status-transfer-history',
                config: {
                    url: '/current-status-transfer-history',
                    templateUrl: 'app/views/transfer-history/transferHistory.html',
                    controller: 'transferHistoryController as vm',
                    title: 'Transfer History'
                }
            },
            //-------------Cost Guard History-------
            {
                state: 'current-status-tab.cost-guard',
                config: {
                    url: '/cost-guard',
                    templateUrl: 'app/views/transfer-history/costguard.html',
                    controller: 'costguardController as vm',
                    title: 'Cost Guard History'
                }
            },


            //-------------Leave Information-------
            {
                state: 'current-status-tab.leave-information',
                config: {
                    url: '/leave-information',
                    templateUrl: 'app/views/leave-information/leaveInformation.html',
                    controller: 'leaveInformationController as vm',
                    title: 'Leave Information'
                }
            },
            {
                state: 'current-status-leave-information',
                config: {
                    url: '/current-status-leave-information',
                    templateUrl: 'app/views/leave-information/leaveInformation.html',
                    controller: 'leaveInformationController as vm',
                    title: 'Leave Information'
                }
            },

            //-------------Family Information-------
            {
                state: 'current-status-tab.family-information',
                config: {
                    url: '/family-information',
                    templateUrl: 'app/views/family-information/familyInformation.html',
                    controller: 'familyInformationController as vm',
                    title: 'Family Information'
                }
            },


            //-------------Remark-------
            {
                state: 'current-status-tab.remarks',
                config: {
                    url: '/remarks/:type',
                    templateUrl: 'app/views/remark/remarks.html',
                    controller: 'remarksController as vm',
                    title: 'Remark'
                }
            },
            {
                state: 'current-status-tab.remark-create',
                config: {
                    url: '/remark-create/:type',
                    templateUrl: 'app/views/remark/remarkAdd.html',
                    controller: 'remarkAddController as vm',
                    title: 'Remark'
                }
            },
            {
                state: "current-status-tab.remark-modify",
                config: {
                    url: "remark-modify/:type/:id",
                    templateUrl: 'app/views/remark/remarkAdd.html',
                    controller: 'remarkAddController as vm',
                    title: 'Remark'
                }
            },


            //-------------Course Future Plan-------
            {
                state: 'current-status-tab.course-future-plans',
                config: {
                    url: '/course-future-plans',
                    templateUrl: 'app/views/course-future-plan/courseFuturePlans.html',
                    controller: 'courseFuturePlansController as vm',
                    title: 'Course Future Plan'
                }
            },
            {
                state: 'current-status-tab.course-future-plan-create',
                config: {
                    url: '/course-future-plan-create',
                    templateUrl: 'app/views/course-future-plan/courseFuturePlanAdd.html',
                    controller: 'courseFuturePlanAddController as vm',
                    title: 'Course Future Plan'
                }
            },
            {
                state: "current-status-tab.course-future-plan-modify",
                config: {
                    url: 'course-future-plan-modify/:id',
                    templateUrl: 'app/views/course-future-plan/courseFuturePlanAdd.html',
                    controller: 'courseFuturePlanAddController as vm',
                    title: 'Course Future Plan'
                }
            },


            //-------------Transfer Future Plan-------
            {
                state: 'current-status-tab.transfer-future-plans',
                config: {
                    url: '/transfer-future-plans',
                    templateUrl: 'app/views/transfer-future-plan/transferFuturePlans.html',
                    controller: 'transferFuturePlansController as vm',
                    title: 'Transfer Future Plan'
                }
            },
            {
                state: 'current-status-tab.transfer-future-plan-create',
                config: {
                    url: '/transfer-future-plan-create',
                    templateUrl: 'app/views/transfer-future-plan/transferFuturePlanAdd.html',
                    controller: 'transferFuturePlanAddController as vm',
                    title: 'Transfer Future Plan'
                }
            },
            {
                state: "current-status-tab.transfer-future-plan-modify",
                config: {
                    url: 'transfer-future-plan-modify/:id',
                    templateUrl: 'app/views/transfer-future-plan/transferFuturePlanAdd.html',
                    controller: 'transferFuturePlanAddController as vm',
                    title: 'Transfer Future Plan'
                }
            },


            //-------------Promotion Policy-------
            {
                state: 'promotion-policies',
                config: {
                    url: '/promotion-policies',
                    templateUrl: 'app/views/promotion-policy/promotionPolicies.html',
                    controller: 'promotionPoliciesController as vm',
                    title: 'Promotion Policy'
                }
            },

            {
                state: 'promotion-policy-create',
                config: {
                    url: '/promotion-policy-create',
                    templateUrl: 'app/views/promotion-policy/promotionPolicyAdd.html',
                    controller: 'promotionPolicyAddController as vm',
                    title: 'Promotion Policy'
                }
            },
            {
                state: "promotion-policy-modify",
                config: {
                    url: "promotion-policy-modify/:id",
                    templateUrl: 'app/views/promotion-policy/promotionPolicyAdd.html',
                    controller: 'promotionPolicyAddController as vm',
                    title: 'Promotion Policy'
                }
            },

            //-------------Trace Settings-------
            {
                state: 'trace-settings',
                config: {
                    url: '/trace-settings?ps&pn&q',
                    templateUrl: 'app/views/trace-setting/traceSettings.html',
                    controller: 'traceSettingsController as vm',
                    title: 'Trace Settings'
                }
            },
            {
                state: 'trace-setting-create',
                config: {
                    url: '/trace-setting-create',
                    templateUrl: 'app/views/trace-setting/traceSettingAdd.html',
                    controller: 'traceSettingAddController as vm',
                    title: 'Trace Settings'
                }
            },
            {
                state: "trace-setting-tabs.trace-setting-modify",
                config: {
                    url: "/trace-setting-modify/:traceSettingId",
                    templateUrl: 'app/views/trace-setting/traceSettingAdd.html',
                    controller: 'traceSettingAddController as vm',
                    title: 'Trace Settings'
                }
            },

            //-------------Trace Settings Tab-------
            {
                state: 'trace-setting-tabs',
                config: {
                    url: '/trace-setting-tabs?id',
                    templateUrl: 'app/views/trace-setting-tab/index.html',
                    controller: 'traceSettingTabController as vm',
                    title: 'Trace Settings Tab'
                }
            },
            //-------------Course Points-------
            {
                state: 'trace-setting-tabs.course-points',
                config: {
                    url: '/course-points',
                    templateUrl: 'app/views/trace-setting-course-point/coursePoints.html',
                    controller: 'coursePointsController as vm',
                    title: 'Course Points'
                }
            },
            {
                state: 'trace-setting-tabs.course-point-create',
                config: {
                    url: '/course-point-create/:traceSettingId/:coursePointId',
                    templateUrl: 'app/views/trace-setting-course-point/coursePointAdd.html',
                    controller: 'coursePointAddController as vm',
                    title: 'Course Points'
                }
            },

            {
                state: 'trace-setting-tabs.course-point-modify',
                config: {
                    url: '/course-point-modify/:traceSettingId/:coursePointId',
                    templateUrl: 'app/views/trace-setting-course-point/coursePointAdd.html',
                    controller: 'coursePointAddController as vm',
                    title: 'Course Points'
                }
            },
            //-------------Poor Course Result-------
            {
                state: 'trace-setting-tabs.poor-course-results',
                config: {
                    url: '/poor-course-results',
                    templateUrl: 'app/views/trace-setting-poor-course-result/poorCourseResults.html',
                    controller: 'poorCourseResultsController as vm',
                    title: 'Poor Course Result'
                }
            },
            {
                state: 'trace-setting-tabs.poor-course-result-create',
                config: {
                    url: '/poor-course-result-create/:traceSettingId/:poorCourseResultId',
                    templateUrl: 'app/views/trace-setting-poor-course-result/poorCourseResultAdd.html',
                    controller: 'poorCourseResultAddController as vm',
                    title: 'Poor Course Result'
                }
            },

            {
                state: 'trace-setting-tabs.poor-course-result-modify',
                config: {
                    url: '/poor-course-result-modify/:traceSettingId/:poorCourseResultId',
                    templateUrl: 'app/views/trace-setting-poor-course-result/poorCourseResultAdd.html',
                    controller: 'poorCourseResultAddController as vm',
                    title: 'Poor Course Result'
                }
            },
            //-------------Branch & Country  Wise Course Point-------
            {
                state: 'trace-setting-tabs.branch-country-course-points',
                config: {
                    url: '/branch-country-course-points',
                    templateUrl: 'app/views/trace-setting-bra-ctry-course-point/braCtryCoursePoints.html',
                    controller: 'braCtryCoursePointsController as vm',
                    title: 'Branch & Country  Wise Course Point'
                }
            },
            {
                state: 'trace-setting-tabs.branch-country-course-point-create',
                config: {
                    url: '/branch-country-course-point-create/:traceSettingId/:braCtryCoursePointId',
                    templateUrl: 'app/views/trace-setting-bra-ctry-course-point/braCtryCoursePointAdd.html',
                    controller: 'braCtryCoursePointAddController as vm',
                    title: 'Branch & Country  Wise Course Point'
                }
            },

            {
                state: 'trace-setting-tabs.branch-country-course-point-modify',
                config: {
                    url: '/branch-country-course-point-modify/:traceSettingId/:braCtryCoursePointId',
                    templateUrl: 'app/views/trace-setting-bra-ctry-course-point/braCtryCoursePointAdd.html',
                    controller: 'braCtryCoursePointAddController as vm',
                    title: 'Branch & Country  Wise Course Point'
                }
            },
            //-------------Point Deduction Punishment-------
            {
                state: 'trace-setting-tabs.point-deduction-punishments',
                config: {
                    url: '/point-deduction-punishments',
                    templateUrl: 'app/views/trace-setting-point-deduct-punishment/ptDeductPunishments.html',
                    controller: 'ptDeductPunishmentsController as vm',
                    title: 'Point Deduction Punishment'
                }
            },
            {
                state: 'trace-setting-tabs.point-deduction-punishment-create',
                config: {
                    url: '/point-deduction-punishment-create/:traceSettingId/:ptDeductPunishmentId',
                    templateUrl: 'app/views/trace-setting-point-deduct-punishment/ptDeductPunishmentAdd.html',
                    controller: 'ptDeductPunishmentAddController as vm',
                    title: 'Point Deduction Punishment'
                }
            },
            {
                state: 'trace-setting-tabs.point-deduction-punishment-modify',
                config: {
                    url: '/point-deduction-punishment-modify/:traceSettingId/:ptDeductPunishmentId',
                    templateUrl: 'app/views/trace-setting-point-deduct-punishment/ptDeductPunishmentAdd.html',
                    controller: 'ptDeductPunishmentAddController as vm',
                    title: 'Point Deduction Punishment'
                }
            },

            //-------------Bonus Point Medal-----------------------
            {
                state: 'trace-setting-tabs.bonus-point-medals',
                config: {
                    url: '/bonus-point-medals',
                    templateUrl: 'app/views/trace-setting-bonus-point-medal/bonusPtMedals.html',
                    controller: 'bonusPtMedalsController as vm',
                    title: 'Bonus Point Medal'
                }
            },
            {
                state: 'trace-setting-tabs.bonus-point-medal-create',
                config: {
                    url: '/bonus-point-medal-create/:traceSettingId/:bonusPtMedalId',
                    templateUrl: 'app/views/trace-setting-bonus-point-medal/bonusPtMedalAdd.html',
                    controller: 'bonusPtMedalAddController as vm',
                    title: 'Bonus Point Medal'
                }
            },
            {
                state: 'trace-setting-tabs.bonus-point-medal-modify',
                config: {
                    url: '/bonus-point-medal-modify/:traceSettingId/:bonusPtMedalId',
                    templateUrl: 'app/views/trace-setting-bonus-point-medal/bonusPtMedalAdd.html',
                    controller: 'bonusPtMedalAddController as vm',
                    title: 'Bonus Point Medal'
                }
            },

            //-------------Bonus Point Award-----------------------
            {
                state: 'trace-setting-tabs.bonus-point-awards',
                config: {
                    url: '/bonus-point-awards',
                    templateUrl: 'app/views/trace-setting-bonus-point-award/bonusPtAwards.html',
                    controller: 'bonusPtAwardsController as vm',
                    title: 'Bonus Point Award'
                }
            },
            {
                state: 'trace-setting-tabs.bonus-point-award-create',
                config: {
                    url: '/bonus-point-award-create/:traceSettingId/:bonusPtAwardId',
                    templateUrl: 'app/views/trace-setting-bonus-point-award/bonusPtAwardAdd.html',
                    controller: 'bonusPtAwardAddController as vm',
                    title: 'Bonus Point Award'
                }
            },
            {
                state: 'trace-setting-tabs.bonus-point-award-modify',
                config: {
                    url: '/bonus-point-award-modify/:traceSettingId/:bonusPtAwardId',
                    templateUrl: 'app/views/trace-setting-bonus-point-award/bonusPtAwardAdd.html',
                    controller: 'bonusPtAwardAddController as vm',
                    title: 'Bonus Point Award'
                }
            },
            //-------------Bonus Point Publication-----------------------
            {
                state: 'trace-setting-tabs.bonus-point-publications',
                config: {
                    url: '/bonus-point-publications',
                    templateUrl: 'app/views/trace-setting-bonus-point-publication/bonusPtPublics.html',
                    controller: 'bonusPtPublicsController as vm',
                    title: 'Bonus Point Publication'
                }
            },
            {
                state: 'trace-setting-tabs.bonus-point-publication-create',
                config: {
                    url: '/bonus-point-publication-create/:traceSettingId/:bonusPtPublicId',
                    templateUrl: 'app/views/trace-setting-bonus-point-publication/bonusPtPublicAdd.html',
                    controller: 'bonusPtPublicAddController as vm',
                    title: 'Bonus Point Publication'
                }
            },
            {
                state: 'trace-setting-tabs.bonus-point-publication-modify',
                config: {
                    url: '/bonus-point-publication-modify/:traceSettingId/:bonusPtPublicId',
                    templateUrl: 'app/views/trace-setting-bonus-point-publication/bonusPtPublicAdd.html',
                    controller: 'bonusPtPublicAddController as vm',
                    title: 'Bonus Point Publication'
                }
            },

            //-------------Bonus Point for Commendation and Appreciation-----------------------
            {
                state: 'trace-setting-tabs.bonus-point-com-apps',
                config: {
                    url: '/bonus-point-com-apps',
                    templateUrl: 'app/views/trace-setting-bonus-point-com-app/bonusPtComApps.html',
                    controller: 'bonusPtComAppsController as vm',
                    title: 'Commendation and Appreciation'
                }
            },
            {
                state: 'trace-setting-tabs.bonus-point-com-app-create',
                config: {
                    url: '/bonus-point-com-app-create/:traceSettingId/:bonusPtComAppId',
                    templateUrl: 'app/views/trace-setting-bonus-point-com-app/bonusPtComAppAdd.html',
                    controller: 'bonusPtComAppAddController as vm',
                    title: 'Commendation and Appreciation'
                }
            },
            {
                state: 'trace-setting-tabs.bonus-point-com-app-modify',
                config: {
                    url: '/bonus-point-com-app-modify/:traceSettingId/:bonusPtComAppId',
                    templateUrl: 'app/views/trace-setting-bonus-point-com-app/bonusPtComAppAdd.html',
                    controller: 'bonusPtComAppAddController as vm',
                    title: 'Commendation and Appreciation'
                }
            },
            //-------------OPR Entry-------
            {
                state: 'opr-entries',
                config: {
                    url: '/opr-entries?ps&pn&q',
                    templateUrl: 'app/views/opr-entry/oprEntries.html',
                    controller: 'oprEntriesController as vm',
                    title: 'OPR Entry'
                }
            },

            {
                state: 'opr-entry-create',
                config: {
                    url: '/opr-entry-create',
                    templateUrl: 'app/views/opr-entry/oprEntryAdd.html',
                    controller: 'oprEntryAddController as vm',
                    title: 'OPR Entry'
                }
            },
            {
                state: "opr-entry-modify",
                config: {
                    url: "opr-entry-modify/:id",
                    templateUrl: 'app/views/opr-entry/oprEntryAdd.html',
                    controller: 'oprEntryAddController as vm',
                    title: 'OPR Entry'
                }
            },
            {
                state: "opr-special-appointments",
                config: {
                    url: "opr-special-appointments/:id",
                    templateUrl: 'app/views/opr-entry/oprAppointments.html',
                    controller: 'oprAppointmentsController as vm',
                    title: 'OPR Entry'
                }
            },
            {
                state: "opr-file-upload",
                config: {
                    url: "opr-file-upload/:id",
                    templateUrl: 'app/views/opr-entry/oprFileUpload.html',
                    controller: 'oprFileUploadController as vm',
                    title: 'OPR Entry'
                }

            },



            //-------------Sea Service-------
            {
                state: 'sea-services',
                config: {
                    url: '/sea-services?ps&pn&q',
                    templateUrl: 'app/views/sea-service/seaServices.html',
                    controller: 'seaServicesController as vm',
                    title: 'Sea Service'
                }
            },
            {
                state: 'sea-service-create',
                config: {
                    url: '/sea-service-create',
                    templateUrl: 'app/views/sea-service/seaServiceAdd.html',
                    controller: 'seaServiceAddController as vm',
                    title: 'Sea Service'
                }
            },
            {
                state: "sea-service-modify",
                config: {
                    url: 'sea-service-modify/:id',
                    templateUrl: 'app/views/sea-service/seaServiceAdd.html',
                    controller: 'seaServiceAddController as vm',
                    title: 'Sea Service'
                }
            },



            //-------------Course Future Plan-------
            {
                state: 'course-future-plans',
                config: {
                    url: '/course-future-plans?ps&pn&q',
                    templateUrl: 'app/views/course-future-plan/courseFuturePlans.html',
                    controller: 'courseFuturePlansController as vm',
                    title: 'Course Future Plan'
                }
            },
            {
                state: 'course-future-plan-create',
                config: {
                    url: '/course-future-plan-create',
                    templateUrl: 'app/views/course-future-plan/courseFuturePlanAdd.html',
                    controller: 'courseFuturePlanAddController as vm',
                    title: 'Course Future Plan'
                }
            },
            {
                state: "course-future-plan-modify",
                config: {
                    url: 'course-future-plan-modify/:id',
                    templateUrl: 'app/views/course-future-plan/courseFuturePlanAdd.html',
                    controller: 'courseFuturePlanAddController as vm',
                    title: 'Course Future Plan'
                }
            },


            //-------------Transfer Future Plan-------
            {
                state: 'transfer-future-plans',
                config: {
                    url: '/transfer-future-plans?ps&pn&q',
                    templateUrl: 'app/views/transfer-future-plan/transferFuturePlans.html',
                    controller: 'transferFuturePlansController as vm',
                    title: 'Transfer Future Plan'
                }
            },
            {
                state: 'transfer-future-plan-create',
                config: {
                    url: '/transfer-future-plan-create',
                    templateUrl: 'app/views/transfer-future-plan/transferFuturePlanAdd.html',
                    controller: 'transferFuturePlanAddController as vm',
                    title: 'Transfer Future Plan'
                }
            },
            {
                state: "transfer-future-plan-modify",
                config: {
                    url: 'transfer-future-plan-modify/:id',
                    templateUrl: 'app/views/transfer-future-plan/transferFuturePlanAdd.html',
                    controller: 'transferFuturePlanAddController as vm',
                    title: 'Transfer Future Plan'
                }
            },

            //-------------Certificate-------
            {
                state: 'certificates',
                config: {
                    url: '/certificates?ps&pn&q',
                    templateUrl: 'app/views/certificate/certificates.html',
                    controller: 'certificatesController as vm',
                    title: 'Certificate'
                }
            },
            {
                state: 'certificate-create',
                config: {
                    url: '/certificate-create',
                    templateUrl: 'app/views/certificate/certificateAdd.html',
                    controller: 'certificateAddController as vm',
                    title: 'Certificate'
                }
            },
            {
                state: "certificate-modify",
                config: {
                    url: "certificate-modify/:id",
                    templateUrl: 'app/views/certificate/certificateAdd.html',
                    controller: 'certificateAddController as vm',
                    title: 'Certificate'
                }
            },



            //-------------Retired Employee-------
            {
                state: 'retired-employees',
                config: {
                    url: '/retired-employees?ps&pn&q',
                    templateUrl: 'app/views/retired-employee/retiredEmployees.html',
                    controller: 'retiredEmployeesController as vm',
                    title: 'Retired Employee'
                }
            },
            {
                state: 'retired-employee',
                config: {
                    url: '/retired-employee/:id',
                    templateUrl: 'app/views/retired-employee/retiredEmployee.html',
                    controller: 'retiredEmployeeController as vm',
                    title: 'Retired Employee'
                }
            },
            {
                state: "retired-employee-modify",
                config: {
                    url: "retired-employee-modify/:id",
                    templateUrl: 'app/views/retired-employee/retiredEmployeeAdd.html',
                    controller: 'retiredEmployeeAddController as vm',
                    title: 'Retired Employee'
                }
            },
            //-------------Employee Reports-------
            {
                state: 'employee-reports',
                config: {
                    url: '/employee-reports?ps&pn&q',
                    templateUrl: 'app/views/employee-report/employeeReports.html',
                    controller: 'employeeReportsController as vm',
                    title: 'Employee Report'
                }
            },
            {
                state: 'employee-report-create',
                config: {
                    url: '/employee-report-create',
                    templateUrl: 'app/views/employee-report/employeeReportAdd.html',
                    controller: 'employeeReportAddController as vm',
                    title: 'Employee Report'
                }
            },
            {
                state: "employee-report-modify",
                config: {
                    url: '/employee-report-modify/:id',
                    templateUrl: 'app/views/employee-report/employeeReportAdd.html',
                    controller: 'employeeReportAddController as vm',
                    title: 'Employee Report'
                }
            },

            //-------------Transfer Proposal-------
            {
                state: 'transfer-proposals',
                config: {
                    url: '/transfer-proposals?ps&pn&q',
                    templateUrl: 'app/views/transfer-proposal/transferProposals.html',
                    controller: 'transferProposalsController as vm',
                    title: 'Transfer Proposal'
                }
            },

            {
                state: 'transfer-proposal-create',
                config: {
                    url: '/transfer-proposal-create',
                    templateUrl: 'app/views/transfer-proposal/transferProposalAdd.html',
                    controller: 'transferProposalAddController as vm',
                    title: 'Transfer Proposal'
                }
            },
            {
                state: "transfer-proposal-modify",
                config: {
                    url: "transfer-proposal-modify/:id",
                    templateUrl: 'app/views/transfer-proposal/transferProposalAdd.html',
                    controller: 'transferProposalAddController as vm',
                    title: 'Transfer Proposal'
                }
            },

            //-------------Transfer Proposal Details-------
            {
                state: 'proposal-details',
                config: {
                    url: '/proposal-details?transferProposalId&ps&pn&q',
                    templateUrl: 'app/views/proposal-detail/proposalDetails.html',
                    controller: 'proposalDetailsController as vm',
                    title: 'Transfer Proposal Detail'
                }
            },

            {
                state: 'proposal-detail-create',
                config: {
                    url: '/proposal-detail-create?transferProposalId',
                    templateUrl: 'app/views/proposal-detail/proposalDetailAdd.html',
                    controller: 'proposalDetailAddController as vm',
                    title: 'Transfer Proposal Detail'
                }
            },
            {
                state: "proposal-detail-modify",
                config: {
                    url: "proposal-detail-modify/:id?transferProposalId",
                    templateUrl: 'app/views/proposal-detail/proposalDetailAdd.html',
                    controller: 'proposalDetailAddController as vm',
                    title: 'Transfer Proposal Detail'
                }
            },
            //-------------Transfer Proposal Candidate-------
            {
                state: 'proposal-candidates',
                config: {
                    url: '/proposal-candidates?transferProposalId&proposalDetailId',
                    templateUrl: 'app/views/proposal-candidate/proposalCandidates.html',
                    controller: 'proposalCandidatesController as vm',
                    title: 'Transfer Proposal Candidate'
                }
            },


            //-------------Advance Search-------
            {
                state: 'advance-search',
                config: {
                    url: '/advance-search',
                    templateUrl: 'app/views/advance-search/advanceSearch.html',
                    controller: 'advanceSearchController as vm',
                    title: 'Advance Search'
                }
            },

            //-------------Advance Search Result-------
            {
                state: 'advance-search-result',
                config: {
                    url: '/advance-search-result',
                    templateUrl: 'app/views/advance-search/advanceSearchResult.html',
                    controller: 'advanceSearchResultController as vm',
                    title: 'Advance Search Result'
                }
            },


            //-------------Basic Search-------
            {
                state: 'basic-search',
                config: {
                    url: '/basic-search',
                    templateUrl: 'app/views/basic-search/basicSearch.html',
                    controller: 'basicSearchController as vm',
                    title: 'Basic Search'
                }
            },


            //-------------Dashboard Tab-------
            {
                state: 'dashboard',
                config: {
                    url: '/dashboard',
                    templateUrl: 'app/views/dashboardTab/index.html',
                    controller: 'dashboardTabController as vm',
                    title: 'Dashboard'
                }
            },


            //-------------Dashboard.Outside Navy-------
            {
                state: 'dashboard.outside-navy',
                config: {
                    url: '/outside-navy',
                    templateUrl: 'app/views/dashboard-outside-navy/dashboardOutsideNavy.html',
                    controller: 'dashboardOutsideNavyController as vm',
                    title: 'Dashboard.Outside Navy'
                }
            },
            //-------------Dashboard.Branch-------
            {
                state: 'dashboard.branch',
                config: {
                    url: '/branch',
                    templateUrl: 'app/views/dashboard-branch/dashboardBranch.html',
                    controller: 'dashboardBranchController as vm',
                    title: 'Dashboard.Branch'
                }
            },
            //-------------Dashboard.Admin Authority-------
            {
                state: 'dashboard.admin-authority',
                config: {
                    url: '/admin-authority',
                    templateUrl: 'app/views/dashboard-admin-authority/dashboardAdminAuthority.html',
                    controller: 'dashboardAdminAuthorityController as vm',
                    title: 'Dashboard.Admin Authority'
                }
            },
            //-------------Dashboard.Leave-------
            {
                state: 'dashboard.leave',
                config: {
                    url: '/leave',
                    templateUrl: 'app/views/dashboard-leave/dashboardLeave.html',
                    controller: 'dashboardLeaveController as vm',
                    title: 'Dashboard.Leave'
                }
            },
            //-------------Dashboard.Stream-------
            {
                state: 'dashboard.stream',
                config: {
                    url: '/stream',
                    templateUrl: 'app/views/dashboard-stream/dashboardStream.html',
                    controller: 'dashboardStreamController as vm',
                    title: 'Dashboard.Stream'
                }
            },
            //-------------Dashboard.Office Appointment-------
            {
                state: 'dashboard.office-appointment',
                config: {
                    url: '/office-appointment',
                    templateUrl: 'app/views/dashboard-office-appointment/dashboardOfficeAppointment.html',
                    controller: 'dashboardOfficeAppointmentController as vm',
                    title: 'Dashboard.Office Appointment'
                }
            },


            //-------------Dashboard.Category-------
            {
                state: 'dashboard.category',
                config: {
                    url: '/category',
                    templateUrl: 'app/views/dashboard-category/dashboardCategory.html',
                    controller: 'dashboardCategoryController as vm',
                    title: 'Dashboard.Category'
                }
            },

            //-------------Dashboard.Gender-------
            {
                state: 'dashboard.gender',
                config: {
                    url: '/gender',
                    templateUrl: 'app/views/dashboard-gender/dashboardGender.html',
                    controller: 'dashboardGenderController as vm',
                    title: 'Dashboard.Gender'
                }
            },
            

            //-------------Outside Navy Officer-------
            {
                state: 'outside-navy-officer',
                config: {
                    url: '/outside-navy-officer/:officeId/:rankLevel/:parentId',
                    templateUrl: 'app/views/dashboard-outside-navy/outsideNavyOfficer.html',
                    controller: 'outsideNavyOfficerController as vm',
                    title: 'Outside Navy Officer'
                }
            },

            //-------------Admin Authority Officer-------
            {
                state: 'admin-authority-officer',
                config: {
                    url: '/admin-authority-officer/:officeId/:rankLevel/:type',
                    templateUrl: 'app/views/dashboard-admin-authority/adminAuthorityOfficer.html',
                    controller: 'adminAuthorityOfficerController as vm',
                    title: 'Admin Authority Officer'
                }
            },

            //-------------Leave Officer-------
            {
                state: 'leave-officer',
                config: {
                    url: '/leave-officer/:leaveType/:rankLevel/:type',
                    templateUrl: 'app/views/dashboard-leave/leaveOfficer.html',
                    controller: 'leaveOfficerController as vm',
                    title: 'Leave Officer'
                }
            },
            //-------------Branch Officer-------
            {
                state: 'branch-officer',
                config: {
                    url: '/branch-officer/:rankId/:branch',
                    templateUrl: 'app/views/dashboard-branch/branchOfficer.html',
                    controller: 'branchOfficerController as vm',
                    title: 'Branch Officer'
                }
            },
            //-------------Stream Officer-------
            {
                state: 'stream-officer',
                config: {
                    url: '/stream-officer/:rankId/:branch/:streamId',
                    templateUrl: 'app/views/dashboard-stream/streamOfficer.html',
                    controller: 'streamOfficerController as vm',
                    title: 'Stream Officer'
                }
            },
            //-------------Gender Officer-------
            {
                state: 'gender-officer',
                config: {
                    url: '/gender-officer/:rankId/:branch/:genderId',
                    templateUrl: 'app/views/dashboard-gender/genderOfficer.html',
                    controller: 'genderOfficerController as vm',
                    title: 'Gender Officer'
                }
            },
            //-------------Category Officer-------
            {
                state: 'category-officer',
                config: {
                    url: '/category-officer/:rankId/:branch/:categoryId',
                    templateUrl: 'app/views/dashboard-category/categoryOfficer.html',
                    controller: 'categoryOfficerController as vm',
                    title: 'Category Officer'
                }
            },


            //-------------Ship Movement-------
            {
                state: 'ship-movement',
                config: {
                    url: '/ship-movement',
                    templateUrl: 'app/views/ship-movement/shipMovement.html',
                    controller: 'shipMovementController as vm',
                    title: 'Ship Movement'
                }
            },

            //-------------Daily Process-------
            {
                state: 'daily-processes',
                config: {
                    url: '/daily-processes',
                    templateUrl: 'app/views/daily-process/daily-processes.html',
                    controller: 'dailyProcessesController as vm',
                    title: 'Daily Process'
                }
            },

            //-------------Status Change-------
            {
                state: 'status-changes',
                config: {
                    url: '/status-changes?ps&pn&q',
                    templateUrl: 'app/views/status-change/statusChanges.html',
                    controller: 'statusChangesController as vm',
                    title: 'Status Change'
                }
            },
            {
                state: 'status-change-create',
                config: {
                    url: '/status-change-create',
                    templateUrl: 'app/views/status-change/statusChangeAdd.html',
                    controller: 'statusChangeAddController as vm',
                    title: 'Status Change'
                }
            },
            {
                state: "status-change-modify",
                config: {
                    url: 'status-change-modify/:id',
                    templateUrl: 'app/views/status-change/statusChangeAdd.html',
                    controller: 'statusChangeAddController as vm',
                    title: 'Status Change'
                }
            }
            ,

        //-------------Extra Appointment-------
        {
            state: 'extra-appointments',
                config: {
                    url: '/extra-appointments?ps&pn&q',
                    templateUrl: 'app/views/extra-appointment/extraAppointments.html',
                    controller: 'extraAppointmentsController as vm',
                    title: 'Extra Appointment'
            }
        },
        {
            state: 'extra-appointment-create',
                config: {
                    url: '/extra-appointment-create',
                    templateUrl: 'app/views/extra-appointment/extraAppointmentAdd.html',
                    controller: 'extraAppointmentAddController as vm',
                    title: 'Extra Appointment'
            }
        },
        {
            state: "extra-appointment-modify",
                config: {
                    url: 'extra-appointment-modify/:id',
                    templateUrl: 'app/views/extra-appointment/extraAppointmentAdd.html',
                    controller: 'extraAppointmentAddController as vm',
                    title: 'Extra Appointment'
            }
        },

            //-------------Foreign Project-------
            {
                state: 'foreign-projects',
                config: {
                    url: '/foreign-projects?ps&pn&q',
                    templateUrl: 'app/views/foreign-project/foreignProjects.html',
                    controller: 'foreignProjectsController as vm',
                    title: 'Foreign Project'
                }
            },
            {
                state: 'foreign-project-create',
                config: {
                    url: '/foreign-project-create',
                    templateUrl: 'app/views/foreign-project/foreignProjectAdd.html',
                    controller: 'foreignProjectAddController as vm',
                    title: 'Foreign Project'
                }
            },
            {
                state: "foreign-project-modify",
                config: {
                    url: 'foreign-project-modify/:id',
                    templateUrl: 'app/views/foreign-project/foreignProjectAdd.html',
                    controller: 'foreignProjectAddController as vm',
                    title: 'Foreign Project'
                }
            },


            //-------------Quick Link-------
            {
                state: 'quick-links',
                config: {
                    url: '/quick-links?ps&pn&q',
                    templateUrl: 'app/views/quick-link/quickLinks.html',
                    controller: 'quickLinksController as vm',
                    title: 'Quick Link'
                }
            },
            {
                state: 'quick-link-create',
                config: {
                    url: '/quick-link-create',
                    templateUrl: 'app/views/quick-link/quickLinkAdd.html',
                    controller: 'quickLinkAddController as vm',
                    title: 'Quick Link'
                }
            },
            {
                state: "quick-link-modify",
                config: {
                    url: "quick-link-modify/:id",
                    templateUrl: 'app/views/quick-link/quickLinkAdd.html',
                    controller: 'quickLinkAddController as vm',
                    title: 'Quick Link'
                }
            },

            //OPR Graph
            {
                state: "opr-graph",
                config: {
                    url: "opr-graph",
                    templateUrl: 'app/views/graphical-report/oprGraph.html',
                    controller: 'graphicalReportsController as vm',
                    title: 'OPR Graph'
                }
            },

            //OPR Two Graph
            {
                state: "opr-two-graph",
                config: {
                    url: "opr-two-graph",
                    templateUrl: 'app/views/graphical-report/oprTwoGraph.html',
                    controller: 'graphicalReportsController as vm',
                    title: 'OPR Two Graph'
                }
            },

          


            //Sea Command Service Graph
            {
                state: "sea-command-service-graph",
                config: {
                    url: "sea-command-service-graph",
                    templateUrl: 'app/views/graphical-report/seaCommandGraph.html',
                    controller: 'graphicalReportsController as vm',
                    title: 'Sea Command Service Graph'
                }
            },


            //Sea Service Graph
            {
                state: "sea-service-graph",
                config: {
                    url: "sea-service-graph",
                    templateUrl: 'app/views/graphical-report/seaServiceGraph.html',
                    controller: 'graphicalReportsController as vm',
                    title: 'Sea Service Graph'
                }
            },

            //Trace Graph
            {
                state: "trace-graph",
                config: {
                    url: "trace-graph",
                    templateUrl: 'app/views/graphical-report/traceGraph.html',
                    controller: 'graphicalReportsController as vm',
                    title: 'Trace Graph'
                }
            },

            //Course Result Graph
            {
                state: "course-result-graph",
                config: {
                    url: "course-result-graph",
                    templateUrl: 'app/views/graphical-report/courseResultGraph.html',
                    controller: 'graphicalReportsController as vm',
                    title: 'Course Result Graph'
                }
            },



            //-------------Special Appointment-------
            {
                state: 'special-appointments',
                config: {
                    url: '/special-appointments?ps&pn&q',
                    templateUrl: 'app/views/special-appointment/specialAppointments.html',
                    controller: 'specialAppointmentsController as vm',
                    title: 'Special Appointment'
                }
            },
            {
                state: 'special-appointment-create',
                config: {
                    url: '/special-appointment-create',
                    templateUrl: 'app/views/special-appointment/specialAppointmentAdd.html',
                    controller: 'specialAppointmentAddController as vm',
                    title: 'Special Appointment'
                }
            },
            {
                state: "special-appointment-modify",
                config: {
                    url: "special-appointment-modify/:id",
                    templateUrl: 'app/views/special-appointment/specialAppointmentAdd.html',
                    controller: 'specialAppointmentAddController as vm',
                    title: 'Special Appointment'
                }
            },



            //-------------Employee Hajj Detail-------
            {
                state: 'employee-hajj-detail',
                config: {
                    url: '/employee-hajj-detail?ps&pn&q',
                    templateUrl: 'app/views/employee-hajj-detail/employeeHajjDetails.html',
                    controller: 'employeeHajjDetailsController as vm',
                    title: 'Employee Hajj'
                }
            },
            {
                state: 'employee-hajj-detail-create',
                config: {
                    url: '/employee-hajj-detail-create',
                    templateUrl: 'app/views/employee-hajj-detail/employeeHajjDetailAdd.html',
                    controller: 'employeeHajjDetailAdd as vm',
                    title: 'Employee Hajj'
                }
            },
            {
                state: "employee-hajj-detail-modify",
                config: {
                    url: "employee-hajj-detail-modify/:id",
                    templateUrl: 'app/views/employee-hajj-detail/employeeHajjDetailAdd.html',
                    controller: 'employeeHajjDetailAdd as vm',
                    title: 'Employee Hajj'
                }
            },
            // ------------------- Contact ------------
            {
                state: "contact",
                config: {
                    url: "/contact",
                    templateUrl: 'app/views/contact/contact.html',
                    controller: 'contactController as vm',
                    title: 'Contact'
                }
            },

            //-----------------------password-----------------
            {
                state: "change-password",
                config: {
                    url: "/change-password",
                    templateUrl: 'app/views/password/changePassword.html',
                    controller: 'changePasswordController as vm',
                    title: 'Change Password'
                }
            },
            // ------------------- Feed Back Form ------------
            {
                state: 'feed-back-form',
                config: {
                    url: '/feed-back-form',
                    templateUrl: 'app/views/feedback-form/feedbackForm.html',
                    controller: 'feedbackFormController as vm',
                    title: 'Feed Back Form'
                }
            },
            {
                state: 'feed-back',
                config: {
                    url: '/feed-back?ps&pn&q',
                    templateUrl: 'app/views/feedback-form/feedBack.html',
                    controller: 'feedBackController as vm',
                    title: 'Feed Back'
                }
            },
            // ------------------- Useful Link ------------
            {
                state: 'useful-link',
                config: {
                    url: '/useful-link',
                    templateUrl: 'app/views/useful-link/useful-link.html',
                    controller: 'usefulLinkController as vm',
                    title: 'Useful Link'
                }
            },
            // ------------------- Notifications ------------
            {
                state: 'notifications',
                config: {
                    url: '/notifications',
                    templateUrl: 'app/views/notifications/notifications.html',
                    controller: 'notificationsController as vm',
                    title: 'Notifications'
                }
            },
            {
                state: 'log-out',
                config: {
                    url: '/log-out',
                    templateUrl: 'app/views/log-out/logOut.html',
                    controller: 'logOutController as vm',
                    title: 'Log Out'
                }
            },
            {
                state: 'download',
                config: {
                    url: '/download',
                    templateUrl: 'app/views/Download/Download.html',
                    controller: 'DownloadController as vm',
                    title: 'Download'
                }
            },
            {
                state: 'download-create',
                config: {
                    url: '/download-create',
                    templateUrl: 'app/views/Download/downloadAdd.html',
                    controller: 'downloadAddController as vm',
                    title: 'Download'
                }
            },
            {
                state: "download-modify",
                config: {
                    url: "download-modify/:id",
                    templateUrl: 'app/views/Download/downloadAdd.html',
                    controller: 'downloadAddController as vm',
                    title: 'Download'
                }
            },
            //{
            //    state: 'download-add',
            //    config: {
            //        url: '/download-add/:id',
            //        templateUrl: 'app/views/Download/downloadAdd.html',
            //        controller: 'downloadAddController as vm',
            //        title: 'Download Add'
            //    }
            //},
            {
                state: 'download-list',
                config: {
                    url: '/download-list?ps&pn&q',
                    templateUrl: 'app/views/Download/downloadView.html',
                    controller: 'downloadViewController as vm',
                    title: 'Download List'
                }
            },
            {
                state: 'users-create',
                config: {
                    url: '/users-create?ps&pn&q',
                    templateUrl: 'app/views/bulk-user/bulkUser.html',
                    controller: 'bulkUserController as vm',
                    title: 'User Create'
                }
            },
            //-------------Dashboard Tab-------
            {
                state: 'admin-dashboard',
                config: {
                    url: '/admin-dashboard',
                    templateUrl: 'app/views/dashboardTab/index.html',
                    controller: 'dashboardTabController as vm',
                    title: 'Admin Dashboard'
                }
            },
            //-------------Dashboard Tab-------
            {
                state: 'print-section',
                config: {
                    url: '/print-section',
                    templateUrl: 'app/views/js-report/print.html',
                    controller: 'printController as vm',
                    title: 'Print'
                }
            },
            {
                state: 'migration',
                config: {
                    url: '/migration',
                    templateUrl: 'app/views/Backup-migration/Migration.html',
                    controller: 'MigrationController as vm',
                    title: 'Migration'
                }
            },
            {
                state: 'employee-history',
                config: {
                    url: '/employee-history',
                    templateUrl: 'app/views/employee-history/employeeHistory.html',
                    controller: 'employeeHistoryController as vm',
                    title: 'Employee History'
                }
            },
            {
                state: 'user-log',
                config: {
                    url: '/user-log',
                    templateUrl: 'app/views/user-log/userLog.html',
                    controller: 'userLogController as vm',
                    title: 'User Log'
                }
            },


            {
                state: 'flash-notice',
                config: {
                    url: '/flash-notice',
                    templateUrl: 'app/views/flash-notice/flashNotice.html',
                    controller: 'flashNoticeController as vm',
                    title: 'Flash Notice'
                }
            },
            {
                state: "flash-notice-modify",
                config: {
                    url: "flash-notice-modify/:id",
                    templateUrl: 'app/views/flash-notice/flashNoticeAdd.html',
                    controller: 'flashNoticeAddController as vm',
                    title: 'Flash Notice Modify'
                }
            },
            {
                state: 'flash-notice-create',
                config: {
                    url: '/flash-notice-create',
                    templateUrl: 'app/views/flash-notice/flashNoticeAdd.html',
                    controller: 'flashNoticeAddController as vm',
                    title: 'Flash Notice Add'
                }
            },
            {
                state: 'flash-notice-board',
                config: {
                    url: '/flash-notice-board',
                    templateUrl: 'app/views/flash-notice/flashNoticeForMember.html',
                    controller: 'flashNoticeForMemberController as vm',
                    title: 'Flash Notice Board'
                }
            },
        ];


    }
})();

